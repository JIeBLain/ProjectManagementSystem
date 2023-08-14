using Entities.Models;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace Repository.Extensions.SearchEngineLibrary;

public class EmployeeSearchEngine
{
    private const LuceneVersion Version = LuceneVersion.LUCENE_48;
    private readonly StandardAnalyzer _analyzer;
    private readonly RAMDirectory _directory;
    private readonly IndexWriter _writer;

    public EmployeeSearchEngine()
    {
        _analyzer = new StandardAnalyzer(Version);
        _directory = new RAMDirectory();
        var config = new IndexWriterConfig(Version, _analyzer);
        _writer = new IndexWriter(_directory, config);
    }


    public void AddEmployeesToIndex(IQueryable<Employee> employees)
    {
        foreach (var employee in employees)
        {
            var document = new Document
            {
                new StringField("Id", employee.Id.ToString(), Field.Store.YES),
                new TextField("FirstName", employee.FirstName, Field.Store.YES),
                new TextField("LastName", employee.LastName, Field.Store.YES),
                new TextField("PatronymicName", employee.PatronymicName, Field.Store.YES),
                //new TextField("BirthDate", employee.BirthDate.ToString(), Field.Store.YES),
                //new TextField("Gender", employee.BirthDate.ToString(), Field.Store.YES),
                //new TextField("Email", employee.Email, Field.Store.YES),
                //new TextField("Phone", employee.Phone, Field.Store.YES)
            };

            _writer.AddDocument(document);
        }

        _writer.Commit();
    }

    public IQueryable<Employee> Search(string searchTerm)
    {
        var directoryReader = DirectoryReader.Open(_directory);
        var indexSearcher = new IndexSearcher(directoryReader);

        var fields = new[] { "Id", "FirstName", "LastName", "PatronymicName",/* "BirthDate", "Gender", "Email", "Phone"*/ };


        var queryParser = new MultiFieldQueryParser(Version, fields, _analyzer);
        queryParser.AllowLeadingWildcard = true;
        var query = queryParser.Parse(searchTerm.Trim().ToLower());

        var hits = indexSearcher.Search(query, 1000).ScoreDocs;

        var employees = new List<Employee>();

        foreach (var hit in hits)
        {
            var document = indexSearcher.Doc(hit.Doc);

            employees.Add(new Employee
            {
                Id = new Guid(document.Get("Id")),
                FirstName = document.Get("FirstName"),
                LastName = document.Get("LastName"),
                PatronymicName = document.Get("PatronymicName"),
                //BirthDate = DateTime.Parse(document.Get("BirthDate"), CultureInfo.InvariantCulture),
                //Gender = Enum.Parse<Gender>(document.Get("Gender")),
                //Email = document.Get("Email"),
                //Phone = document.Get("Phone")
            });

        }

        directoryReader.Dispose();

        return employees.AsQueryable();
    }
}