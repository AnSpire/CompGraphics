using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace lr6
{
    public partial class Form1 : Form
    {
        private PrimitiveType _mode = PrimitiveType.Polygon;

        public Form1() => InitializeComponent();

        // ── события GLControl ──────────────────────────────────────────────

        private void GlControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color.FromArgb(255, 46, 46, 46));
            GL.Enable(EnableCap.DepthTest);
        }

        private void GlControl_Resize(object sender, EventArgs e)
        {
            glControl.MakeCurrent();
            SetupCamera();
            glControl.Invalidate();
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            glControl.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            SetupCamera();

            // Ряд 1 — z=0, x от -7.5 до +7.5 с шагом 3
            float first_row_y = 4f;
            draw_parallelepiped(new Vector3(-7.5f,  0f + first_row_y, 0f), 2.5f, 1.5f, 1.5f, Color.CornflowerBlue, _mode);
            draw_cone          (new Vector3(-4.5f, -1f + first_row_y, 0f), 1.0f, 2.5f,  24,  Color.Tomato,         _mode);
            draw_sphere        (new Vector3(-1.5f,  0f + first_row_y, 0f), 1.2f,  16,   24,  Color.LimeGreen,      _mode);
            draw_tetrahedron   (new Vector3( 1.5f, -1f + first_row_y, 0f), 2.0f,             Color.Gold,           _mode);
            draw_pyramid       (new Vector3( 4.5f, -1f + first_row_y, 0f), 2.2f, 2.5f,       Color.MediumPurple,   _mode);
            draw_frustum       (new Vector3( 7.5f, -1f + first_row_y, 0f), 2.2f, 1.1f, 2f,   Color.DarkOrange,     _mode);

            // Ряд 2 (новые фигуры): z = -6, x с шагом 3, y аналогично ряду 1
            float second_row_y = -3f;
            draw_torus          (new Vector3(-12f, second_row_y,      -6f), 1.2f, 0.5f, 32, 16, Color.DeepSkyBlue,  _mode);
            draw_spring         (new Vector3( -8f, second_row_y,      -6f), 0.7f, 0.2f, 0.9f, 4, 20, 10, Color.HotPink, _mode);
            draw_cylinder       (new Vector3( -4f, second_row_y - 1f, -6f), 0.8f, 2.5f, 20,   Color.MediumSeaGreen, _mode);
            draw_pentagon_pyramid(new Vector3(  0f, second_row_y - 1f, -6f), 1.1f, 2.5f,       Color.Orchid,         _mode);

            glControl.SwapBuffers();
        }

        private void RadioWireframe_CheckedChanged(object sender, EventArgs e)
        {
            _mode = radioWireframe.Checked ? PrimitiveType.LineLoop : PrimitiveType.Polygon;
            glControl.Invalidate();
        }

        // ── камера / проекция ──────────────────────────────────────────────

        private void SetupCamera()
        {
            if (glControl.Width == 0 || glControl.Height == 0) return;

            GL.Viewport(0, 0, glControl.Width, glControl.Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            var proj = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(55f),
                (float)glControl.Width / glControl.Height,
                0.1f, 100f);
            GL.LoadMatrix(ref proj);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            var view = Matrix4.LookAt(
                new Vector3(0f, 5f, 14f),
                new Vector3(0f, 0f, -4f),
                Vector3.UnitY);
            GL.LoadMatrix(ref view);
        }

        // ── вспомогательные ───────────────────────────────────────────────

        private static void Vtx(Vector3 v) => GL.Vertex3(v);
        private static void Col(Color c)   => GL.Color3(c);

        // ── функции рисования ─────────────────────────────────────────────

        /// <summary>Прямоугольный параллелепипед с центром в pos</summary>
        private static void draw_parallelepiped(
            Vector3 pos, float w, float h, float d,
            Color color, PrimitiveType mode)
        {
            float hw = w * .5f, hh = h * .5f, hd = d * .5f;

            Vector3[] v = {
                pos + new Vector3(-hw, -hh,  hd),  // 0
                pos + new Vector3( hw, -hh,  hd),  // 1
                pos + new Vector3( hw,  hh,  hd),  // 2
                pos + new Vector3(-hw,  hh,  hd),  // 3
                pos + new Vector3(-hw, -hh, -hd),  // 4
                pos + new Vector3( hw, -hh, -hd),  // 5
                pos + new Vector3( hw,  hh, -hd),  // 6
                pos + new Vector3(-hw,  hh, -hd),  // 7
            };

            int[][] faces = {
                new[]{0,1,2,3},  // перед
                new[]{5,4,7,6},  // зад
                new[]{4,0,3,7},  // лево
                new[]{1,5,6,2},  // право
                new[]{3,2,6,7},  // верх
                new[]{4,5,1,0},  // низ
            };

            Col(color);
            foreach (var f in faces)
            {
                GL.Begin(mode);
                foreach (int i in f) Vtx(v[i]);
                GL.End();
            }
        }

        /// <summary>Конус с основанием в pos, вершиной вверх</summary>
        private static void draw_cone(
            Vector3 pos, float radius, float height, int segs,
            Color color, PrimitiveType mode)
        {
            Col(color);
            Vector3 apex = pos + new Vector3(0f, height, 0f);

            // основание
            GL.Begin(mode);
            for (int i = 0; i < segs; i++)
            {
                float a = MathHelper.TwoPi * i / segs;
                Vtx(pos + new Vector3(radius * (float)Math.Cos(a), 0f, radius * (float)Math.Sin(a)));
            }
            GL.End();

            // боковые треугольники
            for (int i = 0; i < segs; i++)
            {
                float a0 = MathHelper.TwoPi * i       / segs;
                float a1 = MathHelper.TwoPi * (i + 1) / segs;
                GL.Begin(mode);
                Vtx(pos + new Vector3(radius * (float)Math.Cos(a0), 0f, radius * (float)Math.Sin(a0)));
                Vtx(pos + new Vector3(radius * (float)Math.Cos(a1), 0f, radius * (float)Math.Sin(a1)));
                Vtx(apex);
                GL.End();
            }
        }

        /// <summary>Сфера — аппроксимация четырёхугольными патчами</summary>
        private static void draw_sphere(
            Vector3 pos, float radius, int stacks, int slices,
            Color color, PrimitiveType mode)
        {
            Col(color);
            for (int i = 0; i < stacks; i++)
            {
                float ph0 = MathHelper.Pi * i       / stacks - MathHelper.PiOver2;
                float ph1 = MathHelper.Pi * (i + 1) / stacks - MathHelper.PiOver2;
                float cp0 = (float)Math.Cos(ph0), sp0 = (float)Math.Sin(ph0);
                float cp1 = (float)Math.Cos(ph1), sp1 = (float)Math.Sin(ph1);

                for (int j = 0; j < slices; j++)
                {
                    float t0 = MathHelper.TwoPi * j       / slices;
                    float t1 = MathHelper.TwoPi * (j + 1) / slices;

                    GL.Begin(mode);
                    Vtx(pos + new Vector3(radius * cp0 * (float)Math.Cos(t0), radius * sp0, radius * cp0 * (float)Math.Sin(t0)));
                    Vtx(pos + new Vector3(radius * cp0 * (float)Math.Cos(t1), radius * sp0, radius * cp0 * (float)Math.Sin(t1)));
                    Vtx(pos + new Vector3(radius * cp1 * (float)Math.Cos(t1), radius * sp1, radius * cp1 * (float)Math.Sin(t1)));
                    Vtx(pos + new Vector3(radius * cp1 * (float)Math.Cos(t0), radius * sp1, radius * cp1 * (float)Math.Sin(t0)));
                    GL.End();
                }
            }
        }

        /// <summary>Пирамида с квадратным основанием в pos</summary>
        private static void draw_pyramid(
            Vector3 pos, float baseSize, float height,
            Color color, PrimitiveType mode)
        {
            float hs = baseSize * .5f;
            Vector3[] v = {
                pos + new Vector3(-hs, 0f, -hs),  // 0
                pos + new Vector3( hs, 0f, -hs),  // 1
                pos + new Vector3( hs, 0f,  hs),  // 2
                pos + new Vector3(-hs, 0f,  hs),  // 3
                pos + new Vector3( 0f, height, 0f), // 4 вершина
            };

            // основание
            Col(color);
            GL.Begin(mode);
            Vtx(v[0]); Vtx(v[1]); Vtx(v[2]); Vtx(v[3]);
            GL.End();

            // 4 боковых треугольника
            int[][] sides = { new[]{3,2,4}, new[]{2,1,4}, new[]{1,0,4}, new[]{0,3,4} };
            foreach (var f in sides)
            {
                GL.Begin(mode);
                foreach (int i in f) Vtx(v[i]);
                GL.End();
            }
        }

        /// <summary>Усечённая пирамида (трапеция): основание baseSize, верх topSize</summary>
        private static void draw_frustum(
            Vector3 pos, float baseSize, float topSize, float height,
            Color color, PrimitiveType mode)
        {
            float hb = baseSize * .5f, ht = topSize * .5f;
            Vector3[] v = {
                pos + new Vector3(-hb, 0f,     -hb),  // 0 низ
                pos + new Vector3( hb, 0f,     -hb),  // 1
                pos + new Vector3( hb, 0f,      hb),  // 2
                pos + new Vector3(-hb, 0f,      hb),  // 3
                pos + new Vector3(-ht, height, -ht),  // 4 верх
                pos + new Vector3( ht, height, -ht),  // 5
                pos + new Vector3( ht, height,  ht),  // 6
                pos + new Vector3(-ht, height,  ht),  // 7
            };

            int[][] faces = {
                new[]{0,1,2,3},  // нижнее основание
                new[]{7,6,5,4},  // верхнее основание
                new[]{3,2,6,7},  // перед
                new[]{2,1,5,6},  // право
                new[]{1,0,4,5},  // зад
                new[]{0,3,7,4},  // лево
            };

            Col(color);
            foreach (var f in faces)
            {
                GL.Begin(mode);
                foreach (int i in f) Vtx(v[i]);
                GL.End();
            }
        }

        /// <summary>Правильный тетраэдр с основанием в pos</summary>
        private static void draw_tetrahedron(
            Vector3 pos, float size,
            Color color, PrimitiveType mode)
        {
            float R = size / (float)Math.Sqrt(3.0);
            float H = size * (float)Math.Sqrt(2.0 / 3.0);

            Vector3[] v = {
                pos + new Vector3( 0f,         0f,  R),
                pos + new Vector3(-size * .5f, 0f, -R * .5f),
                pos + new Vector3( size * .5f, 0f, -R * .5f),
                pos + new Vector3( 0f,          H,  0f),
            };

            int[][] faces = {
                new[]{0,2,1},
                new[]{0,1,3},
                new[]{1,2,3},
                new[]{0,3,2},
            };

            Col(color);
            foreach (var f in faces)
            {
                GL.Begin(mode);
                foreach (int i in f) Vtx(v[i]);
                GL.End();
            }
        }

        /// <summary>Тор с центром в pos; R — большой радиус, r — радиус трубки</summary>
        private static void draw_torus(
            Vector3 pos, float R, float r, int rings, int sides,
            Color color, PrimitiveType mode)
        {
            Col(color);
            // наклон вокруг оси X — отверстие смотрит в сторону камеры
            float ct = (float)Math.Cos(MathHelper.DegreesToRadians(15f));
            float st = (float)Math.Sin(MathHelper.DegreesToRadians(15f));

            for (int i = 0; i < rings; i++)
            {
                float u0 = MathHelper.TwoPi * i       / rings;
                float u1 = MathHelper.TwoPi * (i + 1) / rings;

                for (int j = 0; j < sides; j++)
                {
                    float v0 = MathHelper.TwoPi * j       / sides;
                    float v1 = MathHelper.TwoPi * (j + 1) / sides;

                    GL.Begin(mode);
                    Vtx(TorusPoint(pos, R, r, u0, v0, ct, st));
                    Vtx(TorusPoint(pos, R, r, u1, v0, ct, st));
                    Vtx(TorusPoint(pos, R, r, u1, v1, ct, st));
                    Vtx(TorusPoint(pos, R, r, u0, v1, ct, st));
                    GL.End();
                }
            }
        }

        private static Vector3 TorusPoint(Vector3 pos, float R, float r, float u, float v, float ct, float st)
        {
            float cv = (float)Math.Cos(v), sv = (float)Math.Sin(v);
            float cu = (float)Math.Cos(u), su = (float)Math.Sin(u);
            float lx = (R + r * cv) * cu;
            float ly = r * sv;
            float lz = (R + r * cv) * su;
            // поворот вокруг X: y' = ly*cos - lz*sin, z' = ly*sin + lz*cos
            return pos + new Vector3(lx, ly * ct - lz * st, ly * st + lz * ct);
        }

        /// <summary>Спираль-пружина; R — радиус витка, rTube — радиус трубки, pitch — высота одного витка</summary>
        private static void draw_spring(
            Vector3 pos, float R, float rTube, float pitch, int turns, int pathSegs, int tubeSegs,
            Color color, PrimitiveType mode)
        {
            Col(color);
            int total  = turns * pathSegs;
            float p    = pitch / MathHelper.TwoPi;   // высота на радиан
            float halfH = pitch * turns * .5f;        // для центровки по Y
            float len  = (float)Math.Sqrt(R * R + p * p); // длина касательной

            for (int i = 0; i < total; i++)
            {
                float t0 = MathHelper.TwoPi * i       / pathSegs;
                float t1 = MathHelper.TwoPi * (i + 1) / pathSegs;

                for (int j = 0; j < tubeSegs; j++)
                {
                    float phi0 = MathHelper.TwoPi * j       / tubeSegs;
                    float phi1 = MathHelper.TwoPi * (j + 1) / tubeSegs;

                    GL.Begin(mode);
                    Vtx(SpringPoint(pos, R, rTube, p, halfH, len, t0, phi0));
                    Vtx(SpringPoint(pos, R, rTube, p, halfH, len, t1, phi0));
                    Vtx(SpringPoint(pos, R, rTube, p, halfH, len, t1, phi1));
                    Vtx(SpringPoint(pos, R, rTube, p, halfH, len, t0, phi1));
                    GL.End();
                }
            }
        }

        private static Vector3 SpringPoint(
            Vector3 pos, float R, float rTube, float p, float halfH, float len, float t, float phi)
        {
            float ct = (float)Math.Cos(t), st = (float)Math.Sin(t);

            // точка на оси спирали
            float cx = R * ct;
            float cy = p * t - halfH;   // центрируем по Y
            float cz = R * st;

            // касательная (нормирована)
            float tx = -R * st / len;
            float ty =  p      / len;
            float tz =  R * ct / len;

            // нормаль к витку (к оси спирали)
            float nx = -ct, ny = 0f, nz = -st;

            // бинормаль B = T × N
            float bx = -p * st / len;
            float by = -R      / len;
            float bz =  p * ct / len;

            float cp = (float)Math.Cos(phi), sp = (float)Math.Sin(phi);
            return pos + new Vector3(
                cx + rTube * (cp * nx + sp * bx),
                cy + rTube * (cp * ny + sp * by),
                cz + rTube * (cp * nz + sp * bz));
        }

        /// <summary>Цилиндр с основанием в pos</summary>
        private static void draw_cylinder(
            Vector3 pos, float radius, float height, int segs,
            Color color, PrimitiveType mode)
        {
            Col(color);

            // нижнее основание
            GL.Begin(mode);
            for (int i = 0; i < segs; i++)
            {
                float a = MathHelper.TwoPi * i / segs;
                Vtx(pos + new Vector3(radius * (float)Math.Cos(a), 0f, radius * (float)Math.Sin(a)));
            }
            GL.End();

            // верхнее основание
            GL.Begin(mode);
            for (int i = 0; i < segs; i++)
            {
                float a = MathHelper.TwoPi * i / segs;
                Vtx(pos + new Vector3(radius * (float)Math.Cos(a), height, radius * (float)Math.Sin(a)));
            }
            GL.End();

            // боковые грани (четырёхугольники)
            for (int i = 0; i < segs; i++)
            {
                float a0 = MathHelper.TwoPi * i       / segs;
                float a1 = MathHelper.TwoPi * (i + 1) / segs;
                GL.Begin(mode);
                Vtx(pos + new Vector3(radius * (float)Math.Cos(a0), 0f,    radius * (float)Math.Sin(a0)));
                Vtx(pos + new Vector3(radius * (float)Math.Cos(a1), 0f,    radius * (float)Math.Sin(a1)));
                Vtx(pos + new Vector3(radius * (float)Math.Cos(a1), height, radius * (float)Math.Sin(a1)));
                Vtx(pos + new Vector3(radius * (float)Math.Cos(a0), height, radius * (float)Math.Sin(a0)));
                GL.End();
            }
        }

        /// <summary>Пирамида с правильным пятиугольником в основании</summary>
        private static void draw_pentagon_pyramid(
            Vector3 pos, float radius, float height,
            Color color, PrimitiveType mode)
        {
            const int n = 5;
            var pts = new Vector3[n];
            for (int i = 0; i < n; i++)
            {
                float a = MathHelper.TwoPi * i / n - MathHelper.PiOver2;
                pts[i] = pos + new Vector3(radius * (float)Math.Cos(a), 0f, radius * (float)Math.Sin(a));
            }
            Vector3 apex = pos + new Vector3(0f, height, 0f);

            Col(color);

            // пятиугольное основание
            GL.Begin(mode);
            for (int i = 0; i < n; i++) Vtx(pts[i]);
            GL.End();

            // 5 боковых треугольников
            for (int i = 0; i < n; i++)
            {
                GL.Begin(mode);
                Vtx(pts[i]);
                Vtx(pts[(i + 1) % n]);
                Vtx(apex);
                GL.End();
            }
        }
    }
}
