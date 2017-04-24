using System.Collections.Generic;
using OpenTK;
using System.Drawing;

namespace PLGAnalyzer
{
    /// <summary>
    /// This class represents a 3D object. This is very useful in case we have a scene with multiple objects
    /// in it and we want to show them all and in some applications even interact with some of them.
    /// </summary>
    class TKObject
    {
        #region Private members

        private List<Vector3> vecList = new List<Vector3>();
        private List<List<Vector3>> myPolygons = new List<List<Vector3>>();
        private int numberOfPolygons = 0;
        private int numberOfVertices = 0;

        #endregion Private members

        #region Properties

        /// <summary>
        /// A list of polygons. Each polygon is represented by a list of vertices.
        /// This property is read only!
        /// </summary>
        public List<List<Vector3>> MyPolygons
        {
            get
            {
                return myPolygons;
            }
        }

        /// <summary>
        /// Represent the number of polygons that construct this object.
        /// </summary>
        public int NumberOfPolygons
        {
            get
            {
                return numberOfPolygons;
            }
            set
            {
                numberOfPolygons = value;
            }
        }

        /// <summary>
        /// Represent the number of vertices the current object is represented by.
        /// </summary>
        public int NumberOfVertices
        {
            get
            {
                return numberOfVertices;
            }
            set
            {
                numberOfVertices = value;
            }
        }

        /// <summary>
        /// Getter for the vertices list
        /// </summary>
        public List<Vector3> VerticesList
        {
            get
            {
                return vecList;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Adds a new polygon defined by its vertices
        /// </summary>
        /// <param name="vertices"></param>
        public void AddPolygon(List<Vector3> vertices)
        {
            // Adds vertices as new polygon to the object
            myPolygons.Add(vertices);
        }

        /// <summary>
        /// Adds a vertex to the vertices list of this specific object
        /// </summary>
        /// <param name="vertex"></param>
        public void AddVertex(Vector3 vertex)
        {
            vecList.Add(vertex);
        }

        #endregion Public Methods
    }
}
