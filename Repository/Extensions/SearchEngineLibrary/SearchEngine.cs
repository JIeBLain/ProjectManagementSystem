using Entities.Models;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace Repository.Extensions.SearchEngineLibrary;

public class SearchEngine
{
    private static RAMDirectory _directory;

    public SearchEngine(IQueryable<Employee> employees)
    {
        Employees = employees;
        Index();
    }

    public static IndexWriter? Writer { get; set; }
    public static IQueryable<Employee>? Employees { get; set; }

    public static void Index()
    {
        const LuceneVersion lv = LuceneVersion.LUCENE_48;
        var analyzer = new StandardAnalyzer(lv);
        _directory = new RAMDirectory();
        var config = new IndexWriterConfig(lv, analyzer);
        Writer = new IndexWriter(_directory, config);

        var guidField = new StringField("Id", "", Field.Store.YES);
        var firstNameField = new TextField("FirstName", "", Field.Store.YES);
        var lastNameField = new TextField("LastName", "", Field.Store.YES);
        var patronymicNameField = new TextField("PatronymicName", "", Field.Store.YES);

        var document = new Document { guidField, firstNameField, lastNameField, patronymicNameField };

        foreach (var employee in Employees)
        {
            guidField.SetStringValue(employee.Id.ToString());
            firstNameField.SetStringValue(employee.FirstName);
            lastNameField.SetStringValue(employee.LastName);
            patronymicNameField.SetStringValue(employee.PatronymicName);

            Writer.AddDocument(document);
        }

        Writer.Commit();
    }

    public static List<Guid> Search(string input)
    {
        const LuceneVersion lv = LuceneVersion.LUCENE_48;
        var analyzer = new StandardAnalyzer(lv);
        var dirReader = DirectoryReader.Open(_directory);
        var searcher = new IndexSearcher(dirReader);

        var fieldNames = new[] { "Id", "FirstName", "LastName", "PatronymicName" };
        var multiFieldQP = new MultiFieldQueryParser(lv, fieldNames, analyzer);
        multiFieldQP.AllowLeadingWildcard = true;
        var query = multiFieldQP.Parse(input.Trim().ToLower());

        var documents = searcher.Search(query, null, 1000).ScoreDocs;

        var result = new List<Guid>();

        foreach (var doc in documents)
        {
            var document = searcher.Doc(doc.Doc);
            var id = document.Get("Id");

            result.Add(new Guid(id));
        }

        dirReader.Dispose();

        return result;
    }
}