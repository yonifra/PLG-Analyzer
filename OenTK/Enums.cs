using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLGAnalyzer
{
    /// <summary>
    /// The selected projection type for the scene.
    /// </summary>
    enum ProjectionType
    {
        Perspective,
        Ortographic,
    };

    /// <summary>
    /// The type of the shading that will be used for the scene.
    /// </summary>
    enum ShadingType
    {
        Smooth,
        Flat,
    };
}
