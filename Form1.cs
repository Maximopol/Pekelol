using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Windows.Forms;
using WindowsFormsApp1.Pekok;
using WindowsFormsApp1.Pekok.Figure;
using WindowsFormsApp1.Pekok.Maths;
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
            updateInfoPentagons();
            initNumericUpDown();
            initSmallDataGrid();
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
            dataGridView1.RowCount = int.Parse(numericUpDown1.Value.ToString()) * int.Parse(numericUpDown2.Value.ToString());
            dataGridView1.Columns[0].HeaderCell.Value = "Номер точки";
            dataGridView1.Columns[1].HeaderCell.Value = "X";
            dataGridView1.Columns[2].HeaderCell.Value = "Y";
            dataGridView1.Columns[3].HeaderCell.Value = "Z";



            dataGridView2.ColumnCount = 4;
            dataGridView2.RowCount = int.Parse(numericUpDown1.Value.ToString()) * int.Parse(numericUpDown2.Value.ToString());
            dataGridView2.Columns[0].HeaderCell.Value = "Номер точки";
            dataGridView2.Columns[Information.RADIUS_COLUMN].HeaderCell.Value = "Радиус";
            dataGridView2.Columns[Information.DEPTH_COLUMN].HeaderCell.Value = "Глубина";
            dataGridView2.Columns[Information.ROTATION_COLUMN].HeaderCell.Value = "Угол поворота относительно Y";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            drawParallepiped();
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
        private void drawPentagon(PointD center, PointD vertex, double height, int ii)
        {
            Pekok.SolidWorks.swDoc.Extension.SelectByID2(TOP, "PLANE", 0, 0, 0, false, 0, null, 0);

            Pekok.SolidWorks.swDoc.SketchManager.InsertSketch(false);

            object[] sss = Pekok.SolidWorks.swDoc.SketchManager.CreatePolygon(center.X, center.Y, center.Z, vertex.X, vertex.Y, vertex.Z, 5, true);

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

        private void setInfoToDataGrid(int nymber, PointD center, PointD vertex, double g)
        {
            dataGridView1[0, nymber - 1].Value = nymber;
            dataGridView1[1, nymber - 1].Value = center.X * 100;
            dataGridView1[2, nymber - 1].Value = center.Y * 100;
            dataGridView1[3, nymber - 1].Value = center.Z * 100;



            dataGridView2[0, nymber - 1].Value = nymber;
            dataGridView2[Information.RADIUS_COLUMN, nymber - 1].Value = Geometry.getDistanceBetweenTwoPoints(center, vertex) * 100;
            dataGridView2[Information.DEPTH_COLUMN, nymber - 1].Value = g * 100;
            dataGridView2[Information.ROTATION_COLUMN, nymber - 1].Value = Geometry.getRotationAngleRelativeToY(center, vertex);
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
                    AO = Math.Pow(150 - 0, 2) + Math.Pow(50 - 0, 2);
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
                        center.X = x * kekX + (kekX - KC) / 2 + (y + 1) * KC;

                        double AOO = Math.Pow(center.X - x * kekX, 2) + Math.Pow(center.Y - y * kekY, 2);


                        AOO = Math.Pow(AOO, 0.5);

                        double MO = (AOO * Math.Sin(angleA1 * Math.PI / 180));


                        vertex.X = center.X;

                        double lg = kekX > kekY ? kekY : kekX;
                        if (MO > lg / 2)
                        {
                            vertex.Y = center.Y + lg / 2 - 0.01;
                        }
                        else
                        {
                            vertex.Y = center.Y + MO - 0.01;
                        }

                        drawPentagon(center, vertex, height, ii);
                        setInfoToDataGrid(ii, center, vertex, height);
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

        private void updateInfoPentagons()
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
                angleA1 = angle - angleA2;

            for (int y = 0; y < countY; y++)
            {
                for (int x = 0; x < countX; x++)
                {

                    double KC = (width / countY) / Math.Tan(angle * Math.PI / 180);

                    center.Y = y * kekY + kekY / 2;
                    center.X = x * kekX + (kekX - KC) / 2 + (y + 1) * KC;

                    double AOO = Math.Pow(center.X - x * kekX, 2) + Math.Pow(center.Y - y * kekY, 2);


                    AOO = Math.Pow(AOO, 0.5);

                    double MO = (AOO * Math.Sin(angleA1 * Math.PI / 180));


                    vertex.X = center.X;

                    double lg = kekX > kekY ? kekY : kekX;
                    if (MO > lg / 2)
                    {
                        vertex.Y = center.Y + lg / 2 - 0.01;
                    }
                    else
                    {
                        vertex.Y = center.Y + MO - 0.01;
                    }
                    setInfoToDataGrid(ii, center, vertex, height);
                    ii++;
                }
            }
        }
        private void drawPentagons()
        {

        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            changeDataGridsSize();
            updateInfoPentagons();
            initNumericUpDown();
            changeSizeSmallDataGrid();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            changeDataGridsSize();
            updateInfoPentagons();
            initNumericUpDown();
            changeSizeSmallDataGrid();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            updateInfoPentagons();
            initNumericUpDown();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            updateInfoPentagons();
            initNumericUpDown();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            updateInfoPentagons();
            initNumericUpDown();
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            updateInfoPentagons();
            initNumericUpDown();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            drawParallepiped();

            int countX, countY;

            int.TryParse(numericUpDown1.Value.ToString(), out countX);
            int.TryParse(numericUpDown2.Value.ToString(), out countY);

            int ii = 1;


            PointD center = new PointD(), vertex = new PointD();

            for (int y = 0; y < countY; y++)
            {
                for (int x = 0; x < countX; x++)
                {
                    center.X = double.Parse(dataGridView1[1, ii - 1].Value.ToString()) / 100;
                    center.Y = double.Parse(dataGridView1[2, ii - 1].Value.ToString()) / 100;
                    center.Z = double.Parse(dataGridView1[3, ii - 1].Value.ToString()) / 100;


                    vertex.Z = center.Z;


                    double angle = double.Parse(dataGridView2[Information.ROTATION_COLUMN, ii - 1].Value.ToString());
                    double radius = double.Parse(dataGridView2[Information.RADIUS_COLUMN, ii - 1].Value.ToString());
                   

                    vertex.Y = center.Y + (radius * Math.Sin((90 - angle) * Math.PI  / 180)) / 100; ;
                    vertex.X = center.X + (radius * Math.Cos((90 - angle) * Math.PI / 180)) / 100; ;
                    //vertex = Geometry.setNewPositionByAngleRotateAndRaduis(center, vertex, radius, angle);

                    //центрольный угол равен =72, знч нужно поворачиватся от 0 до 72
                    //MessageBox.Show("X=" + center.X + " Y=" + center.Y + " Z=" + center.Z, "1");
                    // MessageBox.Show("X=" + vertex.X + " Y=" + vertex.Y + " Z=" + vertex.Z, "2");

                    drawPentagon(center, vertex, double.Parse(dataGridView2[Information.DEPTH_COLUMN, ii - 1].Value.ToString()) / 100, ii);


                    setInfoToDataGrid(ii, center, vertex, double.Parse(dataGridView2[Information.DEPTH_COLUMN, ii - 1].Value.ToString()) / 100);
                    ii++;
                }
            }
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            int countX, countY;

            int.TryParse(numericUpDown1.Value.ToString(), out countX);
            int.TryParse(numericUpDown2.Value.ToString(), out countY);

            double radius = double.Parse(numericUpDown7.Value.ToString());
            int ii = 1;

            for (int y = 0; y < countY; y++)
            {
                for (int x = 0; x < countX; x++)
                {
                    dataGridView2[Information.RADIUS_COLUMN, ii - 1].Value = radius;
                    ii++;
                }
            }
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            int countX, countY;

            int.TryParse(numericUpDown1.Value.ToString(), out countX);
            int.TryParse(numericUpDown2.Value.ToString(), out countY);

            double depth = double.Parse(numericUpDown8.Value.ToString());
            int ii = 1;

            for (int y = 0; y < countY; y++)
            {
                for (int x = 0; x < countX; x++)
                {
                    dataGridView2[Information.DEPTH_COLUMN, ii - 1].Value = depth;
                    ii++;
                }
            }
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            int countX, countY;

            int.TryParse(numericUpDown1.Value.ToString(), out countX);
            int.TryParse(numericUpDown2.Value.ToString(), out countY);

            double rotate = double.Parse(numericUpDown9.Value.ToString());
            int ii = 1;

            for (int y = 0; y < countY; y++)
            {
                for (int x = 0; x < countX; x++)
                {
                    dataGridView2[Information.ROTATION_COLUMN, ii - 1].Value = rotate;
                    ii++;
                }
            }
        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {

        }
        private void initNumericUpDown()
        {
            numericUpDown7.Value = decimal.Parse(dataGridView2[Information.RADIUS_COLUMN, 0].Value.ToString());
            numericUpDown8.Value = decimal.Parse(dataGridView2[Information.DEPTH_COLUMN, 0].Value.ToString());
            numericUpDown9.Value = decimal.Parse(dataGridView2[Information.ROTATION_COLUMN, 0].Value.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int length1 = int.Parse(numericUpDown1.Value.ToString()), length2 = int.Parse(numericUpDown2.Value.ToString());
            for (int i=0;i<length1 ;i++ )
            {
                for (int j = 0; j < length2; j++)
                {
                    //MessageBox.Show(""+( i + length1 * j + 1), "");
                    if(dataGridView3[2, i].Value.ToString().Equals("Да"))
                    {
                        dataGridView2[Information.RADIUS_COLUMN, i + length1 * j].Value = dataGridView3[1, i].Value;
                    }
                                  
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int length1 = int.Parse(numericUpDown1.Value.ToString()), length2 = int.Parse(numericUpDown2.Value.ToString());
            for (int i = 0; i < length2; i++)
            {
                for (int j = 0; j < length1; j++)
                {
                    //MessageBox.Show(""+( i + length1 * j + 1), "");
                    if (dataGridView4[2, i].Value.ToString().Equals("Да"))
                    {
                        dataGridView2[Information.RADIUS_COLUMN, j + length1 * i].Value = dataGridView4[1, i].Value;
                    }

                }
            }
        }
        private void initSmallDataGrid()
        {
            dataGridView3.ColumnCount = 3;

            dataGridView3.Columns[0].HeaderCell.Value = "Номер оси";
            dataGridView3.Columns[1].HeaderCell.Value = "Радиус";
            dataGridView3.Columns[2].HeaderCell.Value = "Менять?";


            dataGridView4.ColumnCount = 3;

            dataGridView4.Columns[0].HeaderCell.Value = "Номер оси";
            dataGridView4.Columns[1].HeaderCell.Value = "Радиус";
            dataGridView4.Columns[2].HeaderCell.Value = "Менять?";
            changeSizeSmallDataGrid();
        }

        private void changeSizeSmallDataGrid()
        {
            int length1 = int.Parse(numericUpDown1.Value.ToString()), length2 = int.Parse(numericUpDown2.Value.ToString());
            dataGridView3.RowCount = 1 + length1;
            dataGridView4.RowCount = 1 + length2;

            double radius = double.Parse(numericUpDown7.Value.ToString());


            for (int i = 1; i <= length1; i++)
            {
                dataGridView3[0, i - 1].Value = i;
                dataGridView3[1, i - 1].Value = radius;
                dataGridView3[2, i - 1].Value = "Да";
            }
            for (int i = 1; i <= length2; i++)
            {
                dataGridView4[0, i - 1].Value = i;
                dataGridView4[1, i - 1].Value = radius;
                dataGridView4[2, i - 1].Value = "Да";
            }

        }
        
    }
}
