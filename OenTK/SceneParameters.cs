using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PLGAnalyzer
{
    public partial class SceneParameters : Form
    {
        public SceneParameters()
        {
            InitializeComponent();
        }

        public SceneParameters(int polyCount, int vertexCount, int objectsCount, string filename)
        {
            InitializeComponent();

            // Set the labels according to the passed parameters
            if (String.IsNullOrEmpty(filename))
            {
                lblFilename.Text = "No file loaded";
            }
            else
            {
                lblFilename.Text = filename;
            }

            lblObjectsNumber.Text = objectsCount.ToString();
            lblVerticesCount.Text = vertexCount.ToString();
            lblPolygonCount.Text = polyCount.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Closes the dialog
            this.Close();
        }
    }
}
