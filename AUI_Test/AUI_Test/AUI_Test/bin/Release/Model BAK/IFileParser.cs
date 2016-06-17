using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//------------------------------------------------------------
using QiHe.CodeLib;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.BinaryDrawingFormat;
using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.SpreadSheet;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Data;
using System.ComponentModel;
using System.Configuration;
using System.Threading;

namespace AUI_Test
{



    public class IFileParser 
    {

        CompoundDocument doc;
        DataGridView dgvCells = new DataGridView();

        List<SourceLine> Lines { get; set; }
        //-----------------------------------------------------------------------------------


        //---------Mình phai dua vao mot duong dan de lay ra nhung line ---------------------------------------------------------------------------

        public string FileExtension
        {
            get { return @".xls"; }
        }


        public string pline;
        public string pathlines
        {
            get
            {
                return pline;
            }
            set
            {
                pline = value;
            }
        }




        //------------------------------------------------------------------------------------

        public string getopen(string path)
        {
            try
            {
                string file = path;
                doc = CompoundDocument.Open(file);
                return file;
            }
            catch(Exception e)
            {
                return "";
                
            }

        }


        //---------gia tri cua cot neu dua duong dan vao dong va cot -------------------------

        public string ValueCell(string Filepath, int dong, int cot)
        {
            Workbook book = Workbook.Load(Filepath);
            Worksheet sheet = book.Worksheets[0];
            string ID = sheet.Cells[dong, cot].StringValue;
            return ID;
        }


        //-------------- dem so cot cua mot file doc len co duong dan -------------------------

        public int CountCoulum(string Path)
        {
            try
            {

                Workbook book = Workbook.Load(Path);
                Worksheet sheet = book.Worksheets[0];
                int cot = sheet.Cells.LastColIndex + 1;
                return cot ;
            }
            catch
            {
                return 0;
            }
        }

        //----------------------------dem so dong voi file doc len voi duong dn --------------------
        public int CountRow(string Path)
        {
            try
            {
                Workbook book = Workbook.Load(Path);
                Worksheet sheet = book.Worksheets[0];
                int dong = sheet.Cells.LastRowIndex + 1;
                return dong;
            }
            catch
            {
                return 0;
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
                dgvCells.RowCount = sheet.Cells.LastRowIndex + 1;
                dgvCells.ColumnCount = sheet.Cells.LastColIndex + 1;

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
                    for (int colIndex = row.FirstColIndex; colIndex <= row.LastColIndex; colIndex++)
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

        // Dem Cot nay dung voi hàm getopen ----------------------------------------------------
        public int ColumnCount
        {
            get
            {
                try
                {
                    byte[] bookdata = doc.GetStreamData("Workbook");
                    if (bookdata == null) return 0;
                    Workbook book = WorkbookDecoder.Decode(new MemoryStream(bookdata));
                    Worksheet sheet = book.Worksheets[0];
                    int cell = sheet.Cells.LastColIndex + 1;
                    doc.Close();
                    return cell;
                }
                catch
                {
                    return 0;
                }

            }
        }

        //-------------Dem Dong dùng voi ham get open  ---------------------

        public int Rowcount
        {
            get
            {
                try
                {
                    byte[] bookdata = doc.GetStreamData("Workbook");
                    if (bookdata == null) return 0;
                    Workbook book = WorkbookDecoder.Decode(new MemoryStream(bookdata));
                    Worksheet sheet = book.Worksheets[0];
                    int row = sheet.Cells.LastRowIndex + 1;
                    doc.Close();
                    return row;
                }
                catch
                {
                    return 0;
                }
            }
        }

        //------------------Lay Gia Tri  o dùng voi ham getopen -------------------------------------------

        public string getcell(int i, int j)
        {
            int dong, cot;
            dong = Rowcount;
            cot = ColumnCount;
            //-------------------------------------------------------------------
            byte[] bookdata = doc.GetStreamData("Workbook");
            if (bookdata == null) return null;
            Workbook book = WorkbookDecoder.Decode(new MemoryStream(bookdata));
            Worksheet sheet = book.Worksheets[0];
            //---------------------------------------------------------------------
            string ID = sheet.Cells[i, j].StringValue;
            doc.Close();
            return ID;
        }

        //----------------------------------Working DIr : thư mục làm viec hiện tại -------------------------

        public string pathworkingdir;
        public string WorkingDir
        {
            get
            {
                pathworkingdir = @".\";
                return pathworkingdir;
            }
            set
            {
                pathworkingdir = value;
            }
        }

        //---------------------------------------------------------------------------------

       


    }
}
