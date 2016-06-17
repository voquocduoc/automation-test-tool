using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.SpreadSheet;
using AUI_Test.ABT.Model;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using QiHe.CodeLib;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.BinaryDrawingFormat;
using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.SpreadSheet;

namespace AUI_Test.ABT.Auto
{
    public class ExcelFileParser : ABT.Model.IFileParser
    {
        private string m_FileName;
        private string m_WorksheetName;

        CompoundDocument doc;
        DataGridView dgvCells = new DataGridView();

        public int Rowlastindex;
        public int Collastindex;

        /// <summary>
        /// default constructor
        /// </summary>
        public ExcelFileParser()
        {
            Lines = new List<SourceLine>();
            m_WorksheetName = Constants.DefaultName;
        }

        /// <summary>
        /// name of the file
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
            set
            {
                m_FileName = value;
                Parse(WorkingDir + value);
            }
        }

        /// <summary>
        /// parsed source line
        /// </summary>
        public List<SourceLine> Lines { get; private set; }

        /// <summary>
        /// new instance copied
        /// </summary>
        public IFileParser NewInstance
        {
            get
            {
                IFileParser newInt = new ExcelFileParser();
                newInt.WorkingDir = WorkingDir;
                return newInt;
            }
        }

        /// <summary>
        /// parse the source file
        /// </summary>
        /// <param name="path">the file path</param>
        /// <returns>return true if parse successfully</returns>
        private bool Parse(string path)
        {
            try
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
                m_WorksheetName = sheet.Name;
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
            catch
            {
                Lines.Clear();
                throw;
            }
            finally
            {
                if (this.FileParsed != null)
                    this.FileParsed();
            }
        }

        //------------------ Get Open ----
        public string getopen(string path)
        {
            try
            {
                string file = path;
                doc = CompoundDocument.Open(file);
                return file;
            }
            catch (Exception e)
            {
                return "";

            }

        }

        //-------------------------------------hiện thị ra man hinh noi dung cua excel -------------
        public DataGridView loadexcel()
        {

            try
            {
                byte[] bookdata = doc.GetStreamData("Workbook"); // Doc Theo Byte
                if (bookdata == null) return null;
                Workbook book = WorkbookDecoder.Decode(new MemoryStream(bookdata));
                Worksheet sheet = book.Worksheets[0];
                TabPage sheetPage = new TabPage(); // Tên Sheet
                dgvCells = new DataGridView(); // Tao Moi Datagridview
                dgvCells.Dock = DockStyle.Fill;
                dgvCells.RowCount = sheet.Cells.LastRowIndex + 30;
                this.Rowlastindex = sheet.Cells.LastRowIndex;
                dgvCells.ColumnCount = 500;
                this.Collastindex = sheet.Cells.LastColIndex;

                // tranverse cells
                foreach (Pair<Pair<int, int>, Cell> cell in sheet.Cells)
                {
                    dgvCells[cell.Left.Right, cell.Left.Left].Value = cell.Right.Value;
                    if (cell.Right.Style.BackColor != Color.White)
                    {
                        dgvCells[cell.Left.Right, cell.Left.Left].Style.BackColor = cell.Right.Style.BackColor;
                    }
                }

                // tranvers rows by Index
                for (int rowIndex = sheet.Cells.FirstRowIndex; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
                {
                    Row row = sheet.Cells.GetRow(rowIndex);
                    for (int colIndex = row.FirstColIndex; colIndex <= 500; colIndex++)
                    {
                        Cell cell = row.GetCell(colIndex);
                        
                    }

                    
                }
                // tranvers rows directly

                foreach (KeyValuePair<int, Row> row in sheet.Cells.Rows)
                {
                    foreach (KeyValuePair<int, Cell> cell in row.Value)
                    {

                    }
                }

                
                doc.Close();
                return dgvCells;


            }
            catch
            {
                return null;
            }

        }

        public int returnlastcol(string path)
        {
            getopen(path);

            byte[] bookdata = doc.GetStreamData("Workbook"); // Doc Theo Byte
           
            Workbook book = WorkbookDecoder.Decode(new MemoryStream(bookdata));
            Worksheet sheet = book.Worksheets[0];            
            int cot = sheet.Cells.LastColIndex;
            doc.Close();
            return cot;
        }

        public int returnlastrow(string path)
        {
            getopen(path);

            byte[] bookdata = doc.GetStreamData("Workbook"); // Doc Theo Byte

            Workbook book = WorkbookDecoder.Decode(new MemoryStream(bookdata));
            Worksheet sheet = book.Worksheets[0];
            int dong = sheet.Cells.LastRowIndex;
            doc.Close();
            return dong;
        }


        /// <summary>
        /// file parsed successfully
        /// </summary>
        public event FileParsed FileParsed;

        /// <summary>
        /// the working dir of the parser
        /// </summary>
        public string WorkingDir { get; set; }

        /// <summary>
        /// default extension of the file parser
        /// </summary>
        public string FileExtension
        {
            get { return @".xls"; }
        }

        /// <summary>
        /// save data back to file
        /// </summary>
        public void Save(string worksheetName = null)
        {
            
            try
            {
                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet(worksheetName == null ? m_WorksheetName : worksheetName);

                for (int i = 0; i < Lines.Count; i++)
                {
                    for (int j = 0; j < Lines[i].ColumnCount; j++)
                        worksheet.Cells[i, j] = new Cell(Lines[i].Columns[j]);
                }

                int workaround = 100 - Lines.Count;
                for (int i = 0; i < workaround; i++)
                    worksheet.Cells[Lines.Count + i, 0] = new Cell(string.Empty);

                workbook.Worksheets.Add(worksheet);
                workbook.Save(WorkingDir + FileName + FileExtension);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// create a new file
        /// </summary>
        /// <param name="fileName">the file name</param>
        public void Create(string fileName)
        {
            m_FileName = fileName;

            if (FileParsed != null)
                FileParsed();
        }
    }
}
