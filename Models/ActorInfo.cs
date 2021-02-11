using System;

namespace TechnoDapperBlazor.Models
{
    public class ActorInfo
    {
        public int ActorID { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public DateTime DoB { get; set; }
        public DateTime DoD { get; set; }
        public string GenderType { get; set; }
    }

    //[DisableConcurrentExecution(5)]
    //[AutomaticRetry(Attempts = 0, LogEvents =false, OnAttemptsExceeded =AttemptsExceededAction.Delete)]
    //public static void SendOneCatalog()
    //{
    //    IEnumerable<ActorInfo> actorInfos = ActorRepo.GetItems();

    //    BuildSendExcel_RxOne(actorInfos);
    //}

    //private static void BuildSendExcel_RxOne(IEnumerable<ActorInfo> actorInfos)
    //{
    //    string fileName = "List" + DateTime.Now + "xlsx";

    //    using SpreadsheetDocument doc = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook);
    //    WorkbookPart work = doc.AddWorkbookPart();
    //    work.Workbook = new Workbook();

    //    WorksheetPart workSheet = work.AddNewPart<WorksheetPart>();
    //    var sheetData = new SheetData();
    //    workSheet.Worksheet = new Worksheet(sheetData);

    //    Sheets sheet = work.Workbook.AppendChild(new Sheets());
    //    Sheet shts = new Sheet { Id = work.GetIdOfPart(workSheet), SheetId = 1, Name = "Sheet1" };

    //    sheet.Append(shts);

    //    Row header = new Row();
    //    List<string> nameHeader = new List<string>
    //    {
    //        "FirstName", "FamilyName", "DoB", "DoD", "Gender"
    //    };

    //    foreach(string nameHead in nameHeader)
    //    {
    //        header.AppendChild(new Cell 
    //        { 
    //            DataType = CellValues.String, 
    //            CellValue = new CellValue(nameHead)
    //        });
    //    }
    //    sheetData.AppendChild(header);

    //    foreach(ActorInfo actor in actorInfos)
    //    {
    //        Row newRow = new Row();

    //        newRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(actor.FirstName ?? "") });
    //        newRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(actor.FamilyName ?? "") });
    //        newRow.AppendChild(new Cell { DataType = CellValues.Date, CellValue = new CellValue(actor.DoB.ToString() ?? "") });
    //        newRow.AppendChild(new Cell { DataType = CellValues.Date, CellValue = new CellValue(actor.DoD.ToString() ?? "") });
    //        newRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(actor.GenderType ?? "") });

    //        sheetData.AppendChild(newRow);
    //    }
    //    work.Workbook.Save();
    //    doc.Dispose();

    //    Attachment attachement = new Attachment(File.Open(fileName, FileMode.Open), fileName)
    //    {
    //        ContentType = new ContentType("application/vnd.ms.excel")
    //    };

    //    string fromEmail = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("ExternalEmail");
    //    //Helper.SendEmail(fromEmail, new List<string> { "abc@data.com" }, new List<string> { "new@data.com", null, new List<Attachment> { attachement }, false);
    //}
}
