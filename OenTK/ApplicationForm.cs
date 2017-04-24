///////////////////////////////////////////////////////////////////////////////////////////////////////
//
// PLG Analyzer Application v1.0 - March 10th, 2011
// ================================================
//
// Designed and Written by:
// Yoni Fraimorice - 015832702
// Wissam Mutlak - 301412789
//
// Please refer to the users manual for more information.
//
///////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics;

namespace PLGAnalyzer
{
    public partial class ApplicationForm : Form
    {
        #region Private members

        private bool loaded = false;        // Notifies us if the viewport is dirty (and should be drawn again
        private float x = 0;                // The x position
        private float y = 0;                // The y position
        private float rotationX = 0;        // The rotation angle in the X axis
        private float rotationY = 0;        // The rotation angle in the Y axis
        private float rotationZ = 0;        // The rotation angle in the Z axis
        private float scalingFactor = 0.0f;

        // Used for frame rate control (Can be discarded if not using Frame Rate display)
        private Stopwatch sw = new Stopwatch();
        private double accumulator = 0;
        private int idleCounter = 0;
        private Color backColor = Color.Black;          // This is the back color of the application window
        private AboutBox1 aboutBox = new AboutBox1();
        private string loadedFilename = string.Empty;
        private List<Vector3> vecList = new List<Vector3>();
        private List<List<Vector3>> myPolygons = new List<List<Vector3>>();
        private List<TKObject> objectsList = new List<TKObject>();     // Holds all the 3D objects in the scene
        private int numberOfVertices = 0;
        private int numberOfPolygons = 0;
        private ShadingType shading = ShadingType.Smooth;
        private ProjectionType projection = ProjectionType.Perspective;
        private bool isWireframe = false;
        private Vector3 scalingVector = new Vector3(1.0f, 1.0f, 1.0f);
        private Vector3 upVector = new Vector3(0.0f, 1.0f, 0.0f);
        private Vector3 dirVector = new Vector3(0.0f, 0.0f, -1.0f);
        private Vector3 posVector = new Vector3(0.0f, 0.0f, 6.0f);
        private Vector3 lightPos = new Vector3(0.0f, 0.0f, 0.0f);
        private bool backfaceCulling = false;
        private bool rotateShape = false;
        private bool rotateEye = false;
        private Matrix4 cameraMatrix = new Matrix4();
        private float minX, minY, minZ, maxX, maxY, maxZ;
        private float avgX, avgY, avgZ;

        // Mouse-related variables
        private Point mouseStart;
        private Point mouseEnd;
        private bool mouseDown = false;
        private bool shiftPressed = false;
        private bool ctrlPressed = false;
        private bool altPressed = false;

        #endregion Private members

        // Main form constructor
        public ApplicationForm()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            GL.ClearColor(backColor);
            fpsLabel.BackColor = backColor;

            float[] position = { lightPos.X, lightPos.Y, lightPos.Z, 15.0f };
            GL.Light(LightName.Light0, LightParameter.Position, position);

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
                fpsLabel.Text = idleCounter.ToString();
                accumulator -= 1000;
                idleCounter = 0;            // Resetting counter
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

        /// <summary>
        /// Animates a rotation of the object - obsolete
        /// </summary>
        /// <param name="milliseconds"></param>
        private void Animate(double milliseconds)
        {
            float deltaRotation = (float)milliseconds / 20.0f;
            rotationY += deltaRotation;
            rotationX += deltaRotation;
            rotationZ += deltaRotation;
            glControl1.Invalidate();
        }

        /// <summary>
        /// Computes the FPS timeslice
        /// </summary>
        /// <returns></returns>
        private double ComputeTimeSlice()
        {
            sw.Stop();
            double timeslice = sw.Elapsed.TotalMilliseconds;
            sw.Reset();
            sw.Start();
            return timeslice;
        }

        /// <summary>
        /// Sets up the viewport according to the window size
        /// </summary>
        private void SetupViewport()
        {
            int w = glControl1.Width;
            int h = glControl1.Height;

            GL.Viewport(0, 0, w, h);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Glu.Perspective(45.0f, (float)w / (float)h, 1.0f, 100.0f);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
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
        /// Main function that renders the scene
        /// </summary>
        private void Render()
        {
            if (!loaded) // Play nice
            {
                return;
            }

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            if (projection == ProjectionType.Ortographic)
            {
                GL.Ortho(-glControl1.Width / 2, glControl1.Width / 2, -glControl1.Height / 2, glControl1.Height / 2, -20, 100);
            }
            else
            {
                Glu.Perspective(45.0f + scalingFactor, (float)glControl1.Width / (float)glControl1.Height, 1.0f, 100.0f);
            }

            // Select & Reset the ModelView matrix
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Initialize the scene lighing
            InitializeLighting(lightPos);

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

            // Set the shader according to the selected shading model
            if (shading == ShadingType.Smooth)
            {
                GL.ShadeModel(ShadingModel.Smooth);
            }
            else
            {
                GL.ShadeModel(ShadingModel.Flat);
            }

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Set the looking position for the camera
            Glu.LookAt(posVector.X, posVector.Y, posVector.Z,
                avgX, avgY, avgZ,
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

            GL.Translate(x, y, 0);                  // Translate the objects according to the x,y coordinates
            GL.Scale(scalingVector);                // Scale the scene according to the scaling vector

            if (!rotateEye)
            {
                GL.Rotate(rotationX, Vector3.UnitX);    // Rotate the scene on the X axis in angle "rotationX"
                GL.Rotate(rotationY, Vector3.UnitY);    // Rotate the scene on the Y axis in angle "rotationY"
                GL.Rotate(rotationZ, Vector3.UnitZ);    // Rotate the scene on the Z axis in angle "rotationZ"
            }
            else
            {
                GL.Rotate(rotationZ, dirVector);
            }

            // If number of objects to draw is zero (or less), no need to draw anything...
            if (objectsList.Count > 0)
            {
                foreach (TKObject obj in objectsList)
                {
                    foreach (List<Vector3> l in obj.MyPolygons)
                    {
                        GL.Normal3(CalcNormal(l[0], l[1], l[2]));  // Setting the normal for the polygons

                        GL.Begin(BeginMode.Polygon);

                        // Add all the vertices that represent the current polygon of the current object
                        for (int i = 0; i < l.Count; i++)
                        {
                            GL.Vertex3(l[i]);
                        }

                        GL.End();
                    }
                }
            }

            // Draw the light bulb to indicate where the light source is
            if (showLightSource.Checked)
            {
                DrawLightBulb(0.02f);
            }

            glControl1.SwapBuffers();
        }

        /// <summary>
        /// Draws an object in the location in which the light source is in space
        /// </summary>
        private void DrawLightBulb(float lightBulbSize)
        {
            GL.Begin(BeginMode.Polygon);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, Color4.Yellow);
            GL.Vertex3(lightPos.X - lightBulbSize, lightPos.Y - lightBulbSize, lightPos.Z - lightBulbSize);
            GL.Vertex3(lightPos.X + lightBulbSize, lightPos.Y - lightBulbSize, lightPos.Z - lightBulbSize);
            GL.Vertex3(lightPos.X - lightBulbSize, lightPos.Y + lightBulbSize, lightPos.Z - lightBulbSize);
            GL.Vertex3(lightPos.X - lightBulbSize, lightPos.Y - lightBulbSize, lightPos.Z + lightBulbSize);
            GL.Vertex3(lightPos.X - lightBulbSize, lightPos.Y + lightBulbSize, lightPos.Z + lightBulbSize);
            GL.Vertex3(lightPos.X + lightBulbSize, lightPos.Y - lightBulbSize, lightPos.Z + lightBulbSize);
            GL.Vertex3(lightPos.X + lightBulbSize, lightPos.Y + lightBulbSize, lightPos.Z - lightBulbSize);
            GL.Vertex3(lightPos.X + lightBulbSize, lightPos.Y + lightBulbSize, lightPos.Z + lightBulbSize);
            GL.End();
        }

        /// <summary>
        /// Calculates a normal vector from three 3D vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <returns></returns>
        Vector3 CalcNormal(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            // For better percision, use type double for the calculations
            double v1x, v1y, v1z, v2x, v2y, v2z;
            double nx, ny, nz;
            double vLen;

            Vector3 Result;

            // Calculate vectors
            v1x = v1.X - v2.X;
            v1y = v1.Y - v2.Y;
            v1z = v1.Z - v2.Z;

            v2x = v2.X - v3.X;
            v2y = v2.Y - v3.Y;
            v2z = v2.Z - v3.Z;

            // Get cross product of vectors
            nx = (v1y * v2z) - (v1z * v2y);
            ny = (v1z * v2x) - (v1x * v2z);
            nz = (v1x * v2y) - (v1y * v2x);

            // Normalise final vector
            vLen = Math.Sqrt((nx * nx) + (ny * ny) + (nz * nz));

            Result.X = Math.Abs((float)(nx / vLen));
            Result.Y = Math.Abs((float)(ny / vLen));
            Result.Z = Math.Abs((float)(nz / vLen));

            return Result;
        }

        /// <summary>
        ///  Handles all the key down events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.NumPad6)
            {
                x += 0.1f;
                lblStatusMessage.Text = "Translate Right (D)";
            }
            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.NumPad4)
            {
                x -= 0.1f;
                lblStatusMessage.Text = "Translate Left (A)";
            }
            else if (e.KeyCode == Keys.W || e.KeyCode == Keys.NumPad8)
            {
                y += 0.1f;
                lblStatusMessage.Text = "Translate Up (W)";
            }
            else if (e.KeyCode == Keys.S || e.KeyCode == Keys.NumPad2)
            {
                y -= 0.1f; ;
                lblStatusMessage.Text = "Translate Down (S)";
            }
            else if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                shiftPressed = true;
            }
            else if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey)
            {
                ctrlPressed = true;
            }
            else if (e.KeyCode == Keys.Z)
            {
                altPressed = true;
            }
            else if (e.KeyCode == Keys.O)
            {
                ZoomIn();
            }
            else if (e.KeyCode == Keys.P)
            {
                ZoomOut();
            }
            else if (e.KeyCode == Keys.M)
            {
                lblStatusMessage.Text = "Rotating eye...";
                rotationX += 1;
            }
            else if (e.KeyCode == Keys.N)
            {
                lblStatusMessage.Text = "Rotating eye...";
                rotationX -= 1;
            }
            else if (e.KeyCode == Keys.Space)
            {
                rotateEye = true;
            }
            else if (e.KeyCode == Keys.R && !rotateEye)
            {
                lblStatusMessage.Text = "Rotating left (Z axis)";
                rotationZ += 1;         // Rotate left
            }
            else if (e.KeyCode == Keys.T && !rotateEye)
            {
                lblStatusMessage.Text = "Rotating right (Z axis)";
                rotationZ -= 1;         // Rotate right
            }
            else if (e.KeyCode == Keys.F && !rotateEye)
            {
                lblStatusMessage.Text = "Rotating left (X axis)";
                rotationX += 1;         // Rotate left
            }
            else if (e.KeyCode == Keys.G && !rotateEye)
            {
                lblStatusMessage.Text = "Rotating right (X axis)";
                rotationX -= 1;         // Rotate right
            }
            else if (e.KeyCode == Keys.V && !rotateEye)
            {
                lblStatusMessage.Text = "Rotating left (Y axis)";
                rotationY -= 1;         // Rotate left
            }
            else if (e.KeyCode == Keys.B && !rotateEye)
            {
                lblStatusMessage.Text = "Rotating right (Y axis)";
                rotationY += 1;         // Rotate right
            }

            else if (e.KeyCode == Keys.I)
            {
                lblStatusMessage.Text = "Rotating left (Z axis)";
                lightPos.Z += 1;         // Move light 

            }
            else if (e.KeyCode == Keys.K)
            {
                lblStatusMessage.Text = "Rotating right (Z axis)";
                lightPos.Z -= 1;          // Move light 
            }
            else if (e.KeyCode == Keys.Y)
            {
                lblStatusMessage.Text = "Rotating left (X axis)";
                lightPos.X += 1;         // Move light 
            }
            else if (e.KeyCode == Keys.H)
            {
                lblStatusMessage.Text = "Rotating right (X axis)";
                lightPos.X -= 1;         // Move light
            }
            else if (e.KeyCode == Keys.U)
            {
                lblStatusMessage.Text = "Rotating left (Y axis)";
                lightPos.Y += 1;       // Move light
            }
            else if (e.KeyCode == Keys.J)
            {
                lblStatusMessage.Text = "Rotating right (Y axis)";
                lightPos.Y -= 1;          // Move light
            }
            else if (e.KeyCode == Keys.Q)
            {
                lblStatusMessage.Text = "Reset performed";
                ResetParameters(false);
                if (!String.IsNullOrEmpty(loadedFilename))
                {
                    Parse(loadedFilename);
                }
            }
            else if (e.KeyCode == Keys.E)
            {
                isWireframe = !isWireframe;
                wireframeToolStripMenuItem.Checked = isWireframe;

                if (isWireframe)
                {
                    lblStatusMessage.Text = "Wireframe mode ON";
                }
                else
                {
                    lblStatusMessage.Text = "Wireframe mode OFF";
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();     // Quits the application
            }
            else
            {
                // If we got an unmapped key, notify the user about this
                lblStatusMessage.Text = "Unknown key command";
            }
        }

        /// <summary>
        /// Zooms into the scene
        /// </summary>
        private void ZoomIn()
        {
            lblStatusMessage.Text = "Zoom In (O)";

            if (scalingFactor > -44.0)
            {
                scalingFactor -= 1.0f;
            }
        }

        /// <summary>
        /// Zooms out from the scene
        /// </summary>
        private void ZoomOut()
        {
            lblStatusMessage.Text = "Zoom Out (P)";

            if (scalingFactor < 134.0f)
            {
                scalingFactor += 1.0f;
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
            cameraMatrix = Matrix4.CreateTranslation(0f, -50f, 0f);

            // Make sure all the parameters are initialized to their default values
            ResetParameters(true);
        }

        /// <summary>
        /// Sets all the lighting and material parameters for the scene
        /// </summary>
        private void InitializeLighting(Vector3 lightPos)
        {
            // Note that Ambient + Diffuse should be equal to 1
            float[] position = { lightPos.X, lightPos.Y, lightPos.Z, 1.0f };
            float[] amb = { 0.8f, 0.5f, 0.4f, 1.0f };
            float[] diff = { 0.2f, 0.5f, 0.6f, 1.0f };
            float[] matAmbient = { 0.3f, 0.5f, 0.1f, 1.0f };
            float[] lmAmbient = { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] matDiffuse = { 0.7f, 0.5f, 0.9f, 1.0f };
            float[] matSpecular = { 1.0f, 1.0f, 1.0f, 1.0f };   // Specular color should be white

            GL.LightModel(LightModelParameter.LightModelLocalViewer, lmAmbient);
            // GL.LightModel(LightModelParameter.LightModelAmbient, lmAmbient);

            GL.Light(LightName.Light0, LightParameter.Position, position);
            GL.Light(LightName.Light0, LightParameter.Ambient, amb);
            GL.Light(LightName.Light0, LightParameter.Diffuse, diff);
            GL.Light(LightName.Light0, LightParameter.Specular, matSpecular);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.DepthTest);

            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, matAmbient);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, matDiffuse);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, matSpecular);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, 70.0f);
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set background color to blue
            SetBGColor(Color.Blue);
        }

        private void cyanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set background color to cyan
            SetBGColor(Color.Cyan);
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set background color to black
            SetBGColor(Color.Black);
        }

        /// <summary>
        /// Sets the background of the viewport to a different color
        /// </summary>
        /// <param name="newColor"></param>
        private void SetBGColor(Color newColor)
        {
            GL.ClearColor(newColor);
            fpsLabel.BackColor = newColor;
            lblStatusMessage.Text = "Background color: " + newColor.ToString();
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set background color to white
            SetBGColor(Color.White);
        }

        private void openPLGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Setting all the parameters for the file load dialog
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "PLG Files|*.plg";
            openFileDialog1.Multiselect = false;

            DialogResult dr = openFileDialog1.ShowDialog(this);

            // If user clicked "OK" in the load file dialog, check if the file is ok
            if (dr == DialogResult.OK)
            {
                ResetParameters(true);
                loadedFilename = openFileDialog1.FileName;

                // Check that the file path is ok and that the file actually exists.
                if (!String.IsNullOrEmpty(loadedFilename) && File.Exists(loadedFilename))
                {
                    // If everything passes, parse it.
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
            // Clearing lists
            myPolygons.Clear();
            vecList.Clear();
            objectsList.Clear();

            // Reseting values to default ones
            numberOfPolygons = 0;
            numberOfVertices = 0;
            avgX = 0;
            avgY = 0;
            avgZ = 0;
            scalingVector = new Vector3(1.0f, 1.0f, 1.0f);
            upVector = new Vector3(0.0f, 1.0f, 0.0f);
            dirVector = new Vector3(0.0f, 0.0f, -1.0f);
            posVector = new Vector3(0.0f, 0.0f, 6.0f);
            lightPos = new Vector3(0.0f, 4.0f, -9.0f);
            rotationX = 0;
            rotationY = 0;
            rotationZ = 0;
            x = 0;
            y = 0;

            // If we are loading a new file, reset some more parameters
            if (resetFilename)
            {
                rotateShape = false;
                loadedFilename = string.Empty;
            }

            lblStatusMessage.Text = "Reset completed successfully!";
        }

        /// <summary>
        /// Parses and extracts information from a given PLG file
        /// </summary>
        /// <param name="p">Path of the file to parse</param>
        private void Parse(string p)
        {
            if (String.IsNullOrEmpty(p))
            {
                return;
            }

            // Initialize total objects count
            int totalObjNumber = 0;

            lblStatusMessage.Text = "Loading PLG file...";

            // Set the text reader to read the file that was passed to it
            TextReader tr = new StreamReader(p);

            // First two strings in the PLG will be used for info on the objects described in the file
            string firstLine = tr.ReadLine();

            if (!String.IsNullOrEmpty(firstLine))
            {
                totalObjNumber = Convert.ToInt32(firstLine);
            }

            // This will be the current line that is being read
            string currentString = tr.ReadLine();
            bool secondPart = false;

            while (!String.IsNullOrEmpty(currentString))
            {
                List<Vector3> verticesList = new List<Vector3>();

                if (currentString.StartsWith("cubes"))
                {
                    // We have a new object, add it to the objects list
                    TKObject obj = new TKObject();

                    secondPart = false;

                    int i = currentString.IndexOf(" ");
                    string tempStr = currentString.Substring(i + 1, currentString.Length - i - 1);
                    int j = tempStr.IndexOf(" ");
                    string tempStr2 = tempStr.Substring(j + 1, tempStr.Length - j - 1);
                    tempStr = tempStr.Remove(j);

                    numberOfVertices += Convert.ToInt32(tempStr);
                    numberOfPolygons += Convert.ToInt32(tempStr2);

                    obj.NumberOfVertices = Convert.ToInt32(tempStr);
                    obj.NumberOfPolygons = Convert.ToInt32(tempStr2);

                    objectsList.Add(obj);
                    currentString = tr.ReadLine();
                }

                // Checking if we reached the second part of the file
                if (!String.IsNullOrEmpty(currentString) && (currentString.StartsWith("0x") && !secondPart))
                {
                    secondPart = true;
                }

                if (!secondPart)        // If we're in the first part of the object description
                {
                    // Get X coordinate
                    int index = currentString.IndexOf(" ");
                    string x = currentString.Substring(0, index);

                    // Get Y coordinate
                    currentString = currentString.Remove(0, x.Length + 1);
                    index = currentString.IndexOf(" ");
                    string y = currentString.Substring(0, index);

                    // Get Z coordinate
                    currentString = currentString.Remove(0, y.Length + 1);
                    string z = currentString;

                    // Add the vertex you just parsed into the vertices list
                    try
                    {
                        objectsList[objectsList.Count - 1].VerticesList.Add(new Vector3((float)Convert.ToDouble(x), (float)Convert.ToDouble(y), (float)Convert.ToDouble(z)));
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
                else      // Second part of object definition
                {
                    // string color = currentString.Substring(0, 6);
                    currentString = currentString.Remove(0, 7);     // Getting rid of 0xffff in the beginning of the line
                    int vertices = Convert.ToInt32(currentString.Substring(0, currentString.IndexOf(" ") + 1));
                    currentString = currentString.Remove(0, currentString.IndexOf(" ") + 1);

                    // Will hold our indices that define the polygon
                    List<int> indices = new List<int>();

                    for (int i = 0; i < vertices - 1; i++)
                    {
                        indices.Add(Convert.ToInt32(currentString.Substring(0, currentString.IndexOf(" ") + 1)));
                        currentString = currentString.Remove(0, currentString.IndexOf(" ") + 1);
                    }

                    indices.Add(Convert.ToInt32(currentString.Substring(0, currentString.Length)));
                    currentString = currentString.Remove(0, currentString.Length);

                    // Build the vertices list that represents the polygon
                    foreach (int i in indices)
                    {
                        verticesList.Add(objectsList[objectsList.Count - 1].VerticesList[i]);
                    }

                    // Add the new polygon to the object
                    objectsList[objectsList.Count - 1].AddPolygon(verticesList);
                }

                // Read the next line in the file
                currentString = tr.ReadLine();
            }

            // Get the minimum / maximum values for X, Y and Z coordinates (for calculating center of mass)
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

            // Calculate center of mass
            avgX = Math.Abs(maxX - minX) / 2;
            avgY = Math.Abs(maxY - minY) / 2;
            avgZ = Math.Abs(maxZ - minZ) / 2;

            lblStatusMessage.Text = "Loading complete...";
        }

        /// <summary>
        /// Adds a new polygon to the currently built object
        /// </summary>
        /// <param name="indices">A list of indices that represent the polygon</param>
        /// <param name="index">The index of the object that will get the new polygon</param>
        private void AddNewPolygon(List<Vector3> indices, TKObject obj)
        {
            obj.AddPolygon(indices);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox.Show(this);
        }

        /// <summary>
        /// Resets the scene by resetting the parameters and reloading the PLG file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetParameters(false);
            Parse(loadedFilename);
        }

        private void wireframeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wireframeToolStripMenuItem.Checked)
            {
                isWireframe = true;
                lblStatusMessage.Text = "Wireframe mode ON";
            }
            else
            {
                isWireframe = false;
                lblStatusMessage.Text = "Wireframe mode OFF";
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
                // Set the shading type to flat shading
                shading = ShadingType.Flat;
                lblStatusMessage.Text = "Flat shading activated";
            }
        }

        private void backfaceCullingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Enables / Disables backface culling
            backfaceCulling = !backfaceCulling;

            if (backfaceCulling)
            {
                lblStatusMessage.Text = "Backface Culling ON";
            }
            else
            {
                lblStatusMessage.Text = "Backface Culling OFF";
            }
        }

        private void sceneParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SceneParameters sceneParams = new SceneParameters(numberOfPolygons, numberOfVertices, objectsList.Count, loadedFilename);
            sceneParams.Show(this);
        }

        private void animateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set rotation to true or false
            rotateShape = !rotateShape;

            // Make sure the checkmark is synced
            animateToolStripMenuItem.Checked = rotateShape;

            // Display message
            if (rotateShape)
            {
                lblStatusMessage.Text = "Rotating scene...";
            }
            else
            {
                lblStatusMessage.Text = "Scene rotation stopped";
            }
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseStart = new Point(e.X, e.Y);
            mouseDown = true;
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            // If some mouse button is pressed, act according to the action it is set
            if (mouseDown)
            {
                if (!shiftPressed)
                {
                    if (!ctrlPressed && !altPressed)
                    {
                        posVector.X += ((mouseStart.X - e.X) * 0.005f);
                        posVector.Y -= ((mouseStart.Y - e.Y) * 0.005f);

                        // Change mouse cursor to Pan cursor
                        string file = Application.StartupPath + @"\Cursors\icon.interactor.pixelshift_c.ico";

                        if (File.Exists(file))
                        {
                            glControl1.Cursor = new Cursor(Application.StartupPath + @"\Cursors\icon.interactor.pixelshift_c.ico");
                        }

                        lblStatusMessage.Text = "Panning scene...";
                    }
                    else
                    {
                        if (ctrlPressed)
                        {
                            glControl1.Cursor = new Cursor(Application.StartupPath + @"\Cursors\icon.interactor.zoom_c.ico");

                            if (mouseStart.Y - e.Y > 0)
                            {
                                ZoomIn();
                            }
                            else
                            {
                                ZoomOut();
                            }
                        }
                        else if (altPressed)
                        {
                            avgX += ((mouseStart.X - e.X) * 0.005f);
                            avgY -= ((mouseStart.Y - e.Y) * 0.005f);

                            // Change mouse cursor to Pan cursor
                            string file = Application.StartupPath + @"\Cursors\icon.interactor.roll_c.ico";

                            if (File.Exists(file))
                            {
                                glControl1.Cursor = new Cursor(Application.StartupPath + @"\Cursors\icon.interactor.roll_c.ico");
                            }

                            lblStatusMessage.Text = "Moving eyepoint...";
                        }
                    }
                }
                else if (!ctrlPressed && !altPressed)
                {
                    if (!rotateEye)
                    {
                        rotationX -= (mouseStart.Y - e.Y);
                        rotationY -= (mouseStart.X - e.X);
                    }
                    else
                    {

                    }

                    // Change mouse cursor to Rotate cursor
                    string file = Application.StartupPath + @"\Cursors\icon.interactor.pan_c.ico";

                    if (File.Exists(file))
                    {
                        glControl1.Cursor = new Cursor(Application.StartupPath + @"\Cursors\icon.interactor.pan_c.ico");
                    }

                    lblStatusMessage.Text = "Rotating scene...";
                }

                mouseStart = e.Location;
                glControl1.Invalidate();
            }
            else
            {
                // No mouse button is pressed - Show default mouse Arrow cursor
                glControl1.Cursor = Cursors.Arrow;
            }
        }

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseEnd = new Point(e.X, e.Y);
            mouseDown = false;
            lblStatusMessage.Text = "Mouse operation ended";
        }

        private void glControl1_KeyUp(object sender, KeyEventArgs e)
        {
            // Check what key was released, and change its flag accordingly
            if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Shift)
            {
                shiftPressed = false;
            }
            else if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey)
            {
                ctrlPressed = false;
            }
            else if (e.KeyCode == Keys.Z)
            {
                altPressed = false;
            }
            else if (e.KeyCode == Keys.Space)
            {
                rotateEye = false;
            }
        }
    }
}
