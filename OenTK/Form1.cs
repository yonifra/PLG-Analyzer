using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Audio;
using System.Diagnostics;

namespace OenTK
{
    public partial class Form1 : Form
    {
        private bool loaded = false;
        private int x = 0;
        private int y = 0;
        private float rotation = 0;
        private Stopwatch sw = new Stopwatch();
        private double accumulator = 0;
        private int idleCounter = 0;
        private Color backColor = Color.Black;
        private AboutBox1 aboutBox = new AboutBox1();
        private string loadedFilename = string.Empty;
        private List<Vector3> vecList = new List<Vector3>();
        List<List<Vector3>> myPolygons = new List<List<Vector3>>();
        private int numberOfObjects = 0;
        private int numberOfVertices = 0;
        private int numberOfPolygons = 0;
        private ShadingType shading = ShadingType.Smooth;
        private ProjectionType projection = ProjectionType.Perspective;
        private bool isWireframe = false;
        private Vector3 scalingVector = new Vector3(1.0f, 1.0f, 1.0f);
        private Vector3 upVector = new Vector3(0.0f, 1.0f, 0.0f);
        private Vector3 dirVector = new Vector3(0.0f, 0.0f, -1.0f);
        private Vector3 posVector = new Vector3(0.0f, 0.0f, 6.0f);
        private bool backfaceCulling = false;
        private bool rotateShape = false;
        private Matrix4 cameraMatrix = new Matrix4();
        private float minX, minY, minZ, maxX, maxY, maxZ;
        private float avgX, avgY, avgZ;

        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            GL.ClearColor(backColor);
            label1.BackColor = backColor;
            SetupViewport();
            Application.Idle += new EventHandler(Application_Idle);
            sw.Start();
        }

        private void Accumulate(double milliseconds)
        {
            idleCounter++;
            accumulator += milliseconds;
            if (accumulator > 1000)
            {
                label1.Text = idleCounter.ToString();
                accumulator -= 1000;
                idleCounter = 0; // don't forget to reset the counter!
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            double milliseconds = ComputeTimeSlice();
            Accumulate(milliseconds);
            
            if (rotateShape)
            {
                Animate(milliseconds);
            }

            glControl1.Invalidate();
        }

        private void Animate(double milliseconds)
        {
            float deltaRotation = (float)milliseconds / 20.0f;
            rotation += deltaRotation;
            glControl1.Invalidate();
        }

        private double ComputeTimeSlice()
        {
            sw.Stop();
            double timeslice = sw.Elapsed.TotalMilliseconds;
            sw.Reset();
            sw.Start();
            return timeslice;
        }

        private void SetupViewport()
        {
            int w = glControl1.Width;
            int h = glControl1.Height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-w / 2, w / 2, -h / 2, h / 2, -20, 100);	// Bottom-left corner pixel has coordinate (0, 0)
            GL.Viewport(0, 0, w, h);						// Use all of the glControl painting area
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!loaded)
            {
                return;
            }

            SetupViewport();
            glControl1.Invalidate();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        /// <summary>
        /// Renders the OpenTK scene
        /// </summary>
        private void Render()
        {
            if (!loaded) // Play nice
            {
                return;
            }

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            //Glu.Perspective(45.0f, glControl1.Width / glControl1.Height, 1.0f, 100.0f);

            if (projection == ProjectionType.Ortographic)
            {
                GL.Ortho(-glControl1.Width / 2, glControl1.Width / 2, -glControl1.Height / 2, glControl1.Height / 2, -20, 100);
            }
            else
            {
                Glu.Perspective(45.0f, glControl1.Width / glControl1.Height, 1.0f, 100.0f);//maxY - minY + 100.0f);
            }

            GL.Enable(EnableCap.DepthTest);

            // Select & Reset the ModelView matrix
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Setup Lighting
            GL.Light(LightName.Light0, LightParameter.Ambient, Color4.Gray);
            GL.Light(LightName.Light0, LightParameter.Diffuse, Color4.LightGray);
            GL.Light(LightName.Light0, LightParameter.Specular, Color4.White);
            GL.Light(LightName.Light0, LightParameter.Position, Color4.Yellow);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            if (glControl1.Focused)
            {
                GL.Color3(Color.Yellow);
            }
            else
            {
                GL.Color3(Color.Blue);
            }

            // If we selected wireframe, draw only the lines, otherwise, fill
            if (isWireframe)
            {
                GL.PolygonMode(MaterialFace.Front, PolygonMode.Line);
                GL.PolygonMode(MaterialFace.Back, PolygonMode.Line);
            }
            else
            {
                GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
                GL.PolygonMode(MaterialFace.Back, PolygonMode.Fill);
            }

            if (shading == ShadingType.Smooth)
            {
                GL.ShadeModel(ShadingModel.Smooth);
            }
            else
            {
                GL.ShadeModel(ShadingModel.Flat);
            }

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LoadIdentity();

            Glu.LookAt(posVector.X, posVector.Y, posVector.Z,
               // avgX, avgY, avgZ,
                posVector.X + dirVector.X, posVector.Y + dirVector.Y, posVector.Z + dirVector.Z,
                upVector.X, upVector.Y, upVector.Z);

            // Perform Backface culling if necessary
            if (backfaceCulling)
            {
                GL.CullFace(CullFaceMode.Front);
                GL.Enable(EnableCap.CullFace);
            }
            else
            {
                GL.Disable(EnableCap.CullFace);
            }

            GL.Translate(x, y, 0);
            GL.Scale(scalingVector);
            GL.Rotate(rotation, Vector3.UnitZ);

            // This is the main drawing loop for OpenTK
            foreach (List<Vector3> l in myPolygons)
            {
                GL.Begin(BeginMode.Polygon);

                for (int i = 0; i < l.Count; i++)
                {
                    GL.Vertex3(l[i]);
                }

                GL.End();
            }

            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.ColorMaterial);

            glControl1.SwapBuffers();
        }

        // Handles all the key down events
        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                x += 1;
                lblStatusMessage.Text = "Translate Right (D)";
            }
            else if (e.KeyCode == Keys.A)
            {
                x -= 1;
                lblStatusMessage.Text = "Translate Left (A)";
            }
            else if (e.KeyCode == Keys.W)
            {
                y += 1;
                lblStatusMessage.Text = "Translate Up (W)";
            }
            else if (e.KeyCode == Keys.S)
            {
                y -= 1; ;
                lblStatusMessage.Text = "Translate Down (S)";
            }
            else if (e.KeyCode == Keys.O)
            {
                lblStatusMessage.Text = "Zoom In (O)";
                scalingVector.X += 0.1f;
                scalingVector.Y += 0.1f;
                scalingVector.Z += 0.1f;

            }
            else if (e.KeyCode == Keys.P)
            {
                lblStatusMessage.Text = "Zoom Out (P)";
                if (scalingVector.X > 0.1f)     // Verifying that we don't reach a zero scaling factor...
                {
                    scalingVector.X -= 0.1f;
                    scalingVector.Y -= 0.1f;
                    scalingVector.Z -= 0.1f;
                }
            }
            else if (e.KeyCode == Keys.Z)
            {
                // TODO: Implement looking right

                lblStatusMessage.Text = "Look Left (Z)";
            }
            else if (e.KeyCode == Keys.X)
            {
                // TODO: Implement looking right

                lblStatusMessage.Text = "Look Right (X)";
            }
            else if (e.KeyCode == Keys.R)
            {
                lblStatusMessage.Text = "Rotating left";
                rotation += 1;  // Rotate left
            }
            else if (e.KeyCode == Keys.T)
            {
                lblStatusMessage.Text = "Rotating right";
                rotation -= 1;      // Rotate right
            }
            else if (e.KeyCode == Keys.Q)
            {
                lblStatusMessage.Text = "Reset performed";
                ResetParameters(false);
                Parse(loadedFilename);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();     // Quits the application
            }
            else
            {
                lblStatusMessage.Text = "Unknown key command";
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GL.ClearDepth(1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            cameraMatrix = Matrix4.Translation(0f, -50f, 0f);

            ResetParameters(true);
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetBGColor(Color.Blue);
        }

        private void cyanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetBGColor(Color.Cyan);
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetBGColor(Color.Black);
        }

        /// <summary>
        /// Sets the background of the viewport to a different color
        /// </summary>
        /// <param name="newColor"></param>
        private void SetBGColor(Color newColor)
        {
            GL.ClearColor(newColor);
            label1.BackColor = newColor;
            lblStatusMessage.Text = "Background color: " + newColor.ToString();
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetBGColor(Color.White);
        }

        private void openPLGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "PLG Files|*.plg";
            openFileDialog1.Multiselect = false;

            DialogResult dr = openFileDialog1.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                ResetParameters(true);

                loadedFilename = openFileDialog1.FileName;
                if (loadedFilename != string.Empty)
                {
                    Parse(loadedFilename);
                }
            }
        }

        /// <summary>
        /// Resets all the parameters back to their default values
        /// </summary>
        /// <param name="resetFilename">True if needs to load a new file, false otherwise (reset, for example)</param>
        private void ResetParameters(bool resetFilename)
        {
            numberOfObjects = 0;
            numberOfPolygons = 0;
            numberOfVertices = 0;
            myPolygons.Clear();
            vecList.Clear();
            avgX = 0;
            avgY = 0;
            avgZ = 0;
            scalingVector.X = 1.0f;
            scalingVector.Y = 1.0f;
            scalingVector.Z = 1.0f;
            rotation = 0;
            x = 0;
            y = 0;
            
            if (resetFilename)
            {
                loadedFilename = string.Empty;
            }

            lblStatusMessage.Text = "Reset performed";
        }

        /// <summary>
        /// Parses and extracts information from a given PLG file
        /// </summary>
        /// <param name="p"></param>
        private void Parse(string p)
        {
            lblStatusMessage.Text = "Loading PLG file...";

            TextReader tr = new StreamReader(p);

            // First two strings in the PLG will be used for info on the objects described in the file
            string firstLine = tr.ReadLine();
            if (firstLine != string.Empty)
            {
                numberOfObjects = Convert.ToInt32(firstLine);       // Represent the number of objects described in the PLG file
            }

            string secondLine = tr.ReadLine();

            // Extract number of polygons and vertices from the PLG file
            if (secondLine != string.Empty)
            {
                int i = secondLine.IndexOf(" ");
                string tempStr = secondLine.Substring(i + 1, secondLine.Length - i - 1);
                int j = tempStr.IndexOf(" ");
                string tempStr2 = tempStr.Substring(j + 1, tempStr.Length - j - 1);
                tempStr = tempStr.Remove(j);

                numberOfVertices = Convert.ToInt32(tempStr);
                numberOfPolygons = Convert.ToInt32(tempStr2);
            }

            // This will be the current line that is being read
            string currentString = tr.ReadLine();
            bool secondPart = false;

            while (currentString != null && currentString != string.Empty)
            {
                // Checking if we reached the second part of the file
                if ((currentString != null) && (currentString.StartsWith("0x") && !secondPart))
                {
                    secondPart = true;
                }

                if (!secondPart)
                {
                    int index = currentString.IndexOf(" ");
                    string x = currentString.Substring(0, index);
                    currentString = currentString.Remove(0, x.Length + 1);
                    index = currentString.IndexOf(" ");
                    string y = currentString.Substring(0, index);
                    currentString = currentString.Remove(0, y.Length + 1);
                    string z = currentString;

                    // Read the values of the vertices from the .plg file
                    try
                    {
                        vecList.Add(new Vector3((float)Convert.ToDouble(x), (float)Convert.ToDouble(y), (float)Convert.ToDouble(z)));
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Could not parse text from file", "Format Exception");
                        break;
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("Value was outside the range of Double", "Overflow Exception");
                        break;
                    }
                }
                else
                {
                    //if (cubes)
                    string color = currentString.Substring(0, 6);
                    currentString = currentString.Remove(0, 7);
                    int vertices = Convert.ToInt32(currentString.Substring(0, currentString.IndexOf(" ") + 1));
                    currentString = currentString.Remove(0, currentString.IndexOf(" ") + 1);

                    List<int> indices = new List<int>();
                    for (int i = 0; i < vertices; i++)
                    {
                        try
                        {
                            if (currentString.IndexOf(" ") == null)
                            {
                                indices.Add(Convert.ToInt32(currentString.Substring(0, currentString.Length)));
                                currentString = currentString.Remove(0, currentString.Length);
                            }
                            else
                            {
                                indices.Add(Convert.ToInt32(currentString.Substring(0, currentString.IndexOf(" ") + 1)));
                                currentString = currentString.Remove(0, currentString.IndexOf(" ") + 1);
                            }
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("FormatException was thrown", "FormatException", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    AddNewPolygon(color, indices);
                }

                currentString = tr.ReadLine();
            }

            foreach (Vector3 v in vecList)
            {
                if (v.X < minX)
                {
                    minX = v.X;
                }
                if (v.Y < minY)
                {
                    minY = v.Y;
                }
                if (v.Z < minZ)
                {
                    minZ = v.Z;
                }
                if (v.X > maxX)
                {
                    maxX = v.X;
                }
                if (v.Y > maxY)
                {
                    maxY = v.Y;
                }
                if (v.Z > maxZ)
                {
                    maxZ = v.Z;
                }
            }
            avgX = Math.Abs(maxX - minX) / 2;
            avgY = Math.Abs(maxY - minY) / 2;
            avgZ = Math.Abs(maxZ - minZ) / 2;

            lblStatusMessage.Text = "Loading complete...";
        }

        private void AddNewPolygon(string color, List<int> indices)
        {
            List<Vector3> vectorList = new List<Vector3>();
            foreach (int i in indices)
            {
                vectorList.Add(vecList[i]);
            }

            myPolygons.Add(vectorList);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox.Show(this);
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetParameters(false);
            Parse(loadedFilename);
        }

        private void wireframeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wireframeToolStripMenuItem.Checked)
            {
                lblStatusMessage.Text = "Wireframe mode enabled";
                isWireframe = true;
            }
            else
            {
                lblStatusMessage.Text = "Wireframe mode disabled";
                isWireframe = false;
            }
        }

        private void perspectiveToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (projection != ProjectionType.Perspective)
            {
                projection = ProjectionType.Perspective;
                lblStatusMessage.Text = "Perspective projection applied";
            }
            else
            {
                lblStatusMessage.Text = "Perspective projection already active";
            }
        }

        private void orthographicToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (projection != ProjectionType.Ortographic)
            {
                projection = ProjectionType.Ortographic;
                lblStatusMessage.Text = "Orthographic projection applied";
            }
            else
            {
                lblStatusMessage.Text = "Orthographic projection already active";
            }
        }

        private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shading != ShadingType.Smooth)
            {
                shading = ShadingType.Smooth;
                lblStatusMessage.Text = "Smooth shading activated";
            }
        }

        private void flatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shading != ShadingType.Flat)
            {
                shading = ShadingType.Flat;
                lblStatusMessage.Text = "Flat shading activated";
            }
        }

        private void backfaceCullingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backfaceCulling = !backfaceCulling;
        }

        private void rotateShapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotateShape = !rotateShape;

            if (rotateShape)
            {
                lblStatusMessage.Text = "Rotating shape";
            }
            else
            {
                lblStatusMessage.Text = "Shape rotation stopped";
            }
        }
    }
}
