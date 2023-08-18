using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace Repository.Extensions.LuceneSearchLibrary;

public class LuceneSearchEngine<T> where T : class
{
    private const LuceneVersion Version = LuceneVersion.LUCENE_48;
    private readonly StandardAnalyzer _analyzer;
    private readonly RAMDirectory _directory;
    private readonly IndexWriter _indexWriter;
    private HashSet<string> _fields;

    public LuceneSearchEngine(IQueryable<T> data)
    {
        _analyzer = new StandardAnalyzer(Version);
        _directory = new RAMDirectory();
        var config = new IndexWriterConfig(Version, _analyzer);
        _indexWriter = new IndexWriter(_directory, config);
        _fields = new HashSet<string>();
        Index(data);
    }

    public List<Document> Search(string searchTerm, int maxResults)
    {
        var directoryReader = DirectoryReader.Open(_directory);
        var indexSearcher = new IndexSearcher(directoryReader);
        var fields = _fields.ToArray();
        var queryParser = new MultiFieldQueryParser(Version, fields, _analyzer);
        queryParser.AllowLeadingWildcard = true;
        var query = queryParser.Parse(searchTerm.Trim().ToLower());
        var hits = indexSearcher.Search(query, maxResults).ScoreDocs;

        var documents = new List<Document>();
        foreach (var hit in hits)
            documents.Add(indexSearcher.Doc(hit.Doc));

        directoryReader.Dispose();

        return documents;
    }

    private void Index(IQueryable<T> data)
    {
        var documents = new List<Document>();

        foreach (var item in data)
        {
            var entityType = item.GetType();
            var propertiesInfo = entityType.GetProperties();
            var document = new Document();

            foreach (var propertyInfo in propertiesInfo)
            {
                var property = propertyInfo.Name;
                _fields.Add(property);

                var value = propertyInfo.GetValue(item);

                if (value is null)
                    continue;

                document.Add(new TextField(property, value.ToString(), Field.Store.YES));
            }

            documents.Add(document);
        }

        _indexWriter.AddDocuments(documents);
        _indexWriter.Commit();
    }
}