using SharpGL;

namespace lr4
{
    public partial class Form1 : Form
    {
        private int _depth = 6;

        // Corner colours: top = sky-blue, bottom-left = purple, bottom-right = orange
        private static readonly float[] _cA = { 0.15f, 0.90f, 1.00f };
        private static readonly float[] _cB = { 0.65f, 0.15f, 1.00f };
        private static readonly float[] _cC = { 1.00f, 0.50f, 0.05f };

        public Form1()
        {
            InitializeComponent();
        }

        private void openGLControl_OpenGLInitialized(object? sender, EventArgs e)
        {
            var gl = openGLControl.OpenGL;
            gl.ClearColor(0.06f, 0.06f, 0.10f, 1.0f);
            gl.ShadeModel(OpenGL.GL_SMOOTH);
        }

        private void openGLControl_Resized(object? sender, EventArgs e)
        {
            SetupProjection();
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            var gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            SetupProjection();

            // Outer equilateral triangle, centred in the viewport
            float h = (float)(Math.Sqrt(3.0) / 2.0);
            float xA =  0.00f, yA =  h * 1.15f;   // top
            float xB = -1.00f, yB = -h * 0.58f;   // bottom-left
            float xC =  1.00f, yC = -h * 0.58f;   // bottom-right

            DrawSierpinski(gl,
                xA, yA, xB, yB, xC, yC,
                _cA, _cB, _cC,
                _depth);

            gl.Flush();
        }

        private void SetupProjection()
        {
            var gl = openGLControl.OpenGL;
            int w = openGLControl.Width;
            int h = openGLControl.Height;
            if (w <= 0 || h <= 0) return;

            gl.Viewport(0, 0, w, h);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            double aspect = (double)w / h;
            if (aspect >= 1.0)
                gl.Ortho(-1.2 * aspect, 1.2 * aspect, -1.2, 1.2, -1.0, 1.0);
            else
                gl.Ortho(-1.2, 1.2, -1.2 / aspect, 1.2 / aspect, -1.0, 1.0);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
        }

        // Recursive Sierpinski triangle with per-vertex colour interpolation.
        // Each leaf triangle inherits blended colours from the three outer corners,
        // producing a smooth gradient across the whole fractal.
        private void DrawSierpinski(OpenGL gl,
            float xA, float yA, float xB, float yB, float xC, float yC,
            float[] cA, float[] cB, float[] cC,
            int n)
        {
            if (n == 0)
            {
                gl.Begin(OpenGL.GL_TRIANGLES);
                gl.Color(cA[0], cA[1], cA[2]); gl.Vertex(xA, yA);
                gl.Color(cB[0], cB[1], cB[2]); gl.Vertex(xB, yB);
                gl.Color(cC[0], cC[1], cC[2]); gl.Vertex(xC, yC);
                gl.End();
                return;
            }

            // Midpoints of sides
            float xAB = (xA + xB) / 2, yAB = (yA + yB) / 2;
            float xBC = (xB + xC) / 2, yBC = (yB + yC) / 2;
            float xCA = (xC + xA) / 2, yCA = (yC + yA) / 2;

            // Midpoint colours (linear blend of endpoint colours)
            float[] cAB = Blend(cA, cB);
            float[] cBC = Blend(cB, cC);
            float[] cCA = Blend(cC, cA);

            // Top sub-triangle
            DrawSierpinski(gl, xA, yA, xAB, yAB, xCA, yCA, cA, cAB, cCA, n - 1);
            // Bottom-left sub-triangle
            DrawSierpinski(gl, xB, yB, xBC, yBC, xAB, yAB, cB, cBC, cAB, n - 1);
            // Bottom-right sub-triangle
            DrawSierpinski(gl, xC, yC, xCA, yCA, xBC, yBC, cC, cCA, cBC, n - 1);
        }

        private static float[] Blend(float[] a, float[] b) =>
            [(a[0] + b[0]) * 0.5f, (a[1] + b[1]) * 0.5f, (a[2] + b[2]) * 0.5f];

        private void trackBarDepth_ValueChanged(object? sender, EventArgs e)
        {
            _depth = trackBarDepth.Value;
            labelDepth.Text = $"Глубина: {_depth}";
        }
    }
}
