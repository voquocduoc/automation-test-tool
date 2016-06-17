using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.SpreadSheet;

namespace AUI_Test
{
    public class ExcelFileParser : IFileParser
    {
        private string m_FileName;


        public ExcelFileParser()
        {
            Lines = new List<SourceLine>();
        }


        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (Parse(WorkingDir + value))
                    m_FileName = value;
            }
        }


        public List<SourceLine> Lines { get; private set; }




        private bool Parse(string path)
        {
            CompoundDocument doc = CompoundDocument.Load(path);
            if (doc == null)
                throw new InvalidOperationException(Constants.Messages.Error_ExcelFileNotFound);

            Lines.Clear();
            byte[] bookdata = doc.GetStreamData("Workbook");
            if (bookdata == null)
                throw new InvalidOperationException(Constants.Messages.Error_ExcelFileNoWorkbook);

            Workbook workbook = WorkbookDecoder.Decode(new MemoryStream(bookdata));
            if (workbook.Worksheets.Count == 0)
                throw new InvalidOperationException(Constants.Messages.Error_ExcelFileNoWorksheet);

            Worksheet sheet = workbook.Worksheets[0];
            for (int rowIndex = sheet.Cells.FirstRowIndex; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
            {
                SourceLine line = new SourceLine();
                Row row = sheet.Cells.GetRow(rowIndex);
                for (int colIndex = row.FirstColIndex; colIndex <= row.LastColIndex; colIndex++)
                {
                    Cell cell = row.GetCell(colIndex);
                    line.Columns.Add(cell.StringValue);
                }
                Lines.Add(line);
            }

            doc.Close();

            return true;
        }


        


    }
}
