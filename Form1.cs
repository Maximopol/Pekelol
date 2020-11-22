using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Windows.Forms;
using WindowsFormsApp1.Pekok;
using WindowsFormsApp1.Pekok.Primitive;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const string FRONT = "Спереди";
        private const string TOP = "Сверху";
        private const string BOTTOM = "Снизу";
        private const string RIGHT = "Справа";
        private const string LEFT = "Слева";

        private bool isCreatedCub;
        public Form1()
        {
            InitializeComponent();
            Pekok.SolidWorks.OpenApp();
            clearDataGrids();
            changeDataGridsSize();
        }

        private void clearDataGrids()
        {
            
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    dataGridView1.Rows[j].Cells[i].Value = "";
                }
            }

            for (int i = 0; i < dataGridView2.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                {
                    dataGridView2.Rows[j].Cells[i].Value = "";
                }
            }
        }
        private void changeDataGridsSize()
        {
           

            dataGridView1.ColumnCount = 4;
            dataGridView1.RowCount = int.Parse(numericUpDown1.Value.ToString())* int.Parse(numericUpDown2.Value.ToString());
            dataGridView1.Columns[0].HeaderCell.Value = "Номер точки";
            dataGridView1.Columns[1].HeaderCell.Value = "X";
            dataGridView1.Columns[2].HeaderCell.Value = "Y";
            dataGridView1.Columns[3].HeaderCell.Value = "Z";


           
            dataGridView2.ColumnCount = 4;
            dataGridView2.RowCount = int.Parse(numericUpDown1.Value.ToString()) * int.Parse(numericUpDown2.Value.ToString());
            dataGridView2.Columns[0].HeaderCell.Value = "Номер точки";
            dataGridView2.Columns[1].HeaderCell.Value = "Радиус";
            dataGridView2.Columns[2].HeaderCell.Value = "Глубина";
            dataGridView2.Columns[3].HeaderCell.Value = "Угол поворота относительно Y";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            drawParallepiped();


            //Pekok.SolidWorks.swDoc.Extension.SelectByID2(TOP, "PLANE", 0, 0, 0, false, 0, null, 0);

            //Pekok.SolidWorks.swDoc.SketchManager.InsertSketch(false);

            //object[] sss = Pekok.SolidWorks.swDoc.SketchManager.CreatePolygon(0, 0, 0, 1, 1, 0, 5, true);

            //foreach (object b in sss)
            //{
            //    ((SketchSegment)b).Select(true);
            //}

            //Pekok.SolidWorks.swDoc.SketchManager.InsertSketch(true);

            //Pekok.SolidWorks.swDoc.Extension.SelectByID2("Sketch1", "SKETCH", 0, 0, 0, false, 0, null, 0);
            //Pekok.SolidWorks.swDoc.ClearSelection2(true);
            //Pekok.SolidWorks.swDoc.Extension.SelectByID2("Sketch1", "SKETCH", 0, 0, 0, false, 4, null, 0);

            //Feature g = Puller.Cut(Pekok.SolidWorks.swDoc, 1);

            //Pekok.SolidWorks.swDoc.ClearSelection();
        }

        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("", "");
        }
        private void drawParallepiped()
        {
            double height, width, length;
            int angle;
            if (
                double.TryParse(numericUpDown3.Value.ToString(), out height)
                & double.TryParse(numericUpDown4.Value.ToString(), out width)
                & double.TryParse(numericUpDown5.Value.ToString(), out length)
                & int.TryParse(numericUpDown6.Value.ToString(), out angle)
                )
            {
                height /= 100;
                width /= 100;
                length /= 100;


                Pekok.SolidWorks.OpenDoc();

                Pekok.SolidWorks.swDoc.ClearSelection();
                Pekok.SolidWorks.swDoc.SketchManager.Insert3DSketch(false);

                Pekok.Figure.Parallelepiped circle = new Pekok.Figure.Parallelepiped(0, 0, 0,
                                                                                length, 0, 0,
                                                                              length + width / Math.Tan(angle * Math.PI / 180), width, 0);

                object[] ss = circle.draw(Pekok.SolidWorks.swDoc.SketchManager);
                foreach (object b in ss)
                {
                    ((SketchSegment)b).Select(true);

                }

                Feature f = Puller.Extrude(Pekok.SolidWorks.swDoc, height);
                if (f != null)
                {
                    isCreatedCub = true;
                }
                Pekok.SolidWorks.swDoc.ClearSelection();             
            }
        }
        private void drawPentagon(PointD center, PointD vertex, double height,int ii)
        {
            Pekok.SolidWorks.swDoc.Extension.SelectByID2(TOP, "PLANE", 0, 0, 0, false, 0, null, 0);

            Pekok.SolidWorks.swDoc.SketchManager.InsertSketch(false);

            object[] sss = Pekok.SolidWorks.swDoc.SketchManager.CreatePolygon(center.X, center.Y, 0, vertex.X, vertex.Y, 0, 5, true);

            foreach (object b in sss)
            {
                ((SketchSegment)b).Select(true);
            }

            Pekok.SolidWorks.swDoc.SketchManager.InsertSketch(true);

            Pekok.SolidWorks.swDoc.Extension.SelectByID2("Sketch" + ii, "SKETCH", 0, 0, 0, false, 0, null, 0);
            Pekok.SolidWorks.swDoc.ClearSelection2(true);
            Pekok.SolidWorks.swDoc.Extension.SelectByID2("Sketch" + ii, "SKETCH", 0, 0, 0, false, 4, null, 0);

            Feature g = Puller.Cut(Pekok.SolidWorks.swDoc, height);

            Pekok.SolidWorks.swDoc.ClearSelection();
        }

        private void setInfoToDataGrid(int nymber,PointD center, PointD vertex, double g)
        {
            dataGridView1[0, nymber-1].Value = nymber;
            dataGridView1[1, nymber - 1].Value = center.X;
            dataGridView1[2, nymber - 1].Value = center.Y;
            dataGridView1[3, nymber - 1].Value = center.Z;



            dataGridView2[0, nymber - 1].Value = nymber;
            dataGridView2[1, nymber - 1].Value = 0;
            dataGridView2[2, nymber - 1].Value =g;
            dataGridView2[3, nymber - 1].Value = 0;

        }
        private void button2_Click(object sender, EventArgs e)
        {

            if (isCreatedCub)
            {
                int angle;
                double height, width, length;
                int.TryParse(numericUpDown6.Value.ToString(), out angle);

                double.TryParse(numericUpDown3.Value.ToString(), out height);

                double.TryParse(numericUpDown4.Value.ToString(), out width);
                double.TryParse(numericUpDown5.Value.ToString(), out length);

                int countX, countY;

                int.TryParse(numericUpDown1.Value.ToString(), out countX);
                int.TryParse(numericUpDown2.Value.ToString(), out countY);

                height /= 100;

                width /= 100;
                length /= 100;

                double kekX = length / countX, kekY = width / countY,
                    addX = width / Math.Tan(angle * Math.PI / 180);


                PointD center = new PointD(), vertex = new PointD();
                int ii = 1;

                double angleA2 = (Math.Atan((width / 2) / (addX + (length - addX) / 2)) * 180 / Math.PI),
                    angleA1 = angle - angleA2,
                    AO =Math.Pow(150-0,2)+ Math.Pow(50-0,2);
                AO = Math.Pow(AO, 0.5);


                //MessageBox.Show("length=" + length + " разбьется на " + countX + "  и длина отрезков =" + kekX +
                //    "\nwidth=" + width + " разбьется на " + countY + "  и длина отрезков =" + kekY +
                //    "\n" + "KC большое=" + addX + " малое=" + (addX / countX)+
                //     "\n"+"Центр точки=("+(addX+(length - addX)/2)+","+(width/2)+")"+
                //     "\n Угол равен a2= "+ angleA2 +
                //     "\n угол a1= "+ angleA1+
                //     "\n длина AO="+ AO+
                //      "\n длина MO=" + (AO*Math.Sin(angleA1 * Math.PI / 180))

                //    , "");


              //  double KC = addX / countX;

                double KC = 0;




                for (int y = 0; y < countY; y++)
                {
                    for (int x = 0; x < countX; x++)
                    {

                        KC = (width / countY) / Math.Tan(angle * Math.PI / 180);

                        center.Y = y * kekY + kekY / 2;
                        center.X = x * kekX + (kekX - KC) / 2 +(y+1)*KC;

                        double AOO= Math.Pow(center.X - x * kekX, 2) + Math.Pow(center.Y - y * kekY, 2);


                        AOO = Math.Pow(AOO, 0.5);

                        double MO = (AOO * Math.Sin(angleA1 * Math.PI / 180));

                         
                        vertex.X = center.X;

                        double lg = kekX > kekY ? kekY : kekX;
                        if (MO> lg / 2)
                        {
                            vertex.Y = center.Y + lg / 2 - 0.01;
                        }
                        else
                        {
                            vertex.Y = center.Y + MO - 0.01;
                        }

                        drawPentagon(center, vertex, height, ii);
                        setInfoToDataGrid(ii,center,vertex, height);
                        ii++;
                    }
                }
                
                //for (int i = 0; i < countY; i++)
                //{
                //    for(int j = 0; j < countX; j++)
                //    {

                //        //center.Y = i * kekY + kekY / 2;

                //        //center.X = j * kekX   + (kekX- addX)/2;

                //        //if (kekX >= kekY)
                //        //{
                //        //    vertex.X = center.X;
                //        //    vertex.Y = center.Y + kekY / 2 - 0.01;
                //        //}
                //        //else
                //        //{
                //        //    vertex.X = center.X;
                //        //    vertex.Y = center.Y + kekX / 2 - 0.01;
                //        //}
                //        drawPentagon(center, vertex, height, ii);
                //        ii++;
                //    }
                //}
            }
            else
            {
                MessageBox.Show("0", "");
            }

            //Pekok.SolidWorks.OpenDoc();

            //Pekok.SolidWorks.swDoc.ClearSelection();
            //Pekok.SolidWorks.swDoc.SketchManager.InsertSketch(false);



            //object[] ss =Pekok.SolidWorks.swDoc.SketchManager.CreatePolygon(0,0,0, 10, 10,10,5, false);

            //Pekok.SolidWorks.swDoc.SketchManager.InsertSketch(true);

            //foreach (object b in ss)
            //{
            //    ((SketchSegment)b).Select(true);

            //}

            //Feature f = Puller.Extrude(Pekok.SolidWorks.swDoc, 100);

            //Pekok.SolidWorks.swDoc.ClearSelection();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            changeDataGridsSize();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            changeDataGridsSize();
        }
    }
}
