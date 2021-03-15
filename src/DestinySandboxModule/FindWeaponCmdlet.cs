namespace DestinySandboxModule
{
    using System.Management.Automation;

    using DestinyLib;
    using DestinyLib.Analysis;
    using DestinyLib.Database;
    using DestinyLib.DataContract;

    [Cmdlet(VerbsCommon.Find, "WeaponCmdlet")]
    [OutputType(typeof(SearchableWeapon))]
    public class FindWeaponCmdlet : PSCmdlet
    {
        private readonly string ConnectionString;
        private readonly AnalysisController AnalysisController;

        public FindWeaponCmdlet()
        {
            var dbPath = LibEnvironment.GetDatabaseFile("world_sql_content");
            this.ConnectionString = Database.MakeConnectionString(dbPath);

            var worldSqlContent = new WorldSqlContent(connectionString: this.ConnectionString);
            this.AnalysisController = new AnalysisController(worldSqlContent);
        }

        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [ValidateNotNull]
        public string SearchString { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            //WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            WriteObject("hello world");

            var results = this.AnalysisController.Search(this.SearchString, AnalysisController.SearchType.Regex);
            
            this.WriteObject(results, true);
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
           // WriteVerbose("End!");
        }
    }
}
