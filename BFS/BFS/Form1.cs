using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BFS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            matrisx_doldur();
            flowlayoutpanel_doldur();
            dfs_olustur();
            bfs_list = new List<node_dfs>();
            bfs_list.Add(dfs_list[29]);
            timer.Start();
            bfs_move(bfs_list[0]);
        }

        int[,] matrisx;

        node_dfs[] dfs_list;
        class node_dfs
        {
            public int x = 0;
            public int y = 0;
            public bool ziyaret_edildimi = false;
            public node_dfs[] komsular = new node_dfs[4] { null, null, null, null };
        }

        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();


        void matrisx_doldur()
        {
            matrisx = new int[31, 28]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },

                {0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,1,0 },
                {0,1,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,1,0 },
                {0,1,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,1,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,1,0 },
                {0,1,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,1,0 },
                {0,1,1,1,1,1,1,0,0,1,1,1,1,0,0,1,1,1,1,0,0,1,1,1,1,1,1,0 },
                {0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,1,1,1,1,1,1,1,1,1,1,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,1,0,0,0,1,1,0,0,0,1,0,0,1,0,0,0,0,0,0 },/*kutu girişi*/
                {0,0,0,0,0,0,1,0,0,1,0,1,1,1,1,1,1,0,1,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,1,1,1,0,1,1,1,1,1,1,0,1,1,1,1,0,0,0,0,0,0 },

                {0,0,0,0,0,0,1,0,0,1,0,1,1,1,1,1,1,0,1,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,1,1,1,1,1,1,1,1,1,1,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,1,0 },
                {0,1,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,1,0 },
                {0,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,0 },
                {0,0,0,1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,0 },
                {0,0,0,1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,0 },
                {0,1,1,1,1,1,1,0,0,1,1,1,1,0,0,1,1,1,1,0,0,1,1,1,1,1,1,0 },
                {0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0 },
                {0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },

                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }

            };

        }

        void flowlayoutpanel_doldur()
        {
            flowLayoutPanel2.Controls.Clear();
            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    PictureBox p = new PictureBox();
                    p.Size = new System.Drawing.Size(20, 20);
                    p.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    p.BackColor = System.Drawing.Color.Transparent;
                    p.Margin = new System.Windows.Forms.Padding(0);
                    p.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    // p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    flowLayoutPanel2.Controls.Add(p);
                }
            }
            //ilk pacmani yerleştir//
            (flowLayoutPanel2.Controls[29] as PictureBox).Image = global::BFS.Properties.Resources.pd;
            //ilk yiyecegi yerlestir//
            (flowLayoutPanel2.Controls[405] as PictureBox).Image = global::BFS.Properties.Resources.strawberry;
        }

        void dfs_olustur()
        {
            dfs_list = new node_dfs[868];
            int a = 0;
            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    dfs_list[a] = new node_dfs();
                    dfs_list[a].x = i;
                    dfs_list[a].y = j;
                    a++;
                }
            }
            //komsularini ekle
            a = 0;
            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    try
                    {
                        if (matrisx[(i), (j - 1)] == 1)
                        {
                            int b = a - 1;
                            for (int k = 0; k < 4; k++)
                            {
                                if (dfs_list[a].komsular[k] == null)
                                {
                                    dfs_list[a].komsular[k] = dfs_list[b];
                                    break;
                                }
                            }
                        }
                        if (matrisx[(i), (j + 1)] == 1)
                        {
                            int b = a + 1;
                            for (int k = 0; k < 4; k++)
                            {
                                if (dfs_list[a].komsular[k] == null)
                                {
                                    dfs_list[a].komsular[k] = dfs_list[b];
                                    break;

                                }
                            }
                        }
                        if (matrisx[(i - 1), (j)] == 1)
                        {
                            int b = a - 28;
                            for (int k = 0; k < 4; k++)
                            {
                                if (dfs_list[a].komsular[k] == null)
                                {
                                    dfs_list[a].komsular[k] = dfs_list[b];
                                    break;

                                }
                            }
                        }
                        if (matrisx[(i + 1), (j)] == 1)
                        {
                            int b = a + 28;
                            for (int k = 0; k < 4; k++)
                            {
                                if (dfs_list[a].komsular[k] == null)
                                {
                                    dfs_list[a].komsular[k] = dfs_list[b];
                                    break;

                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    a++;
                }
            }

        }

        static int bfs_adim = 0;
        static bool bfs_bulundu = false;
        List<node_dfs> bfs_list;

        void bfs_move(node_dfs current)
        {
            int position = current.x * 28 + current.y;
            if (current.ziyaret_edildimi == false)
            {
                if (position != 405 && bfs_bulundu == false)
                {
                    current.ziyaret_edildimi = true;
                    bfs_adim++;
                    for (int i = 0; i < 4; i++)
                    {
                        (flowLayoutPanel2.Controls[(current.x * 28 + current.y)] as PictureBox).Image = global::BFS.Properties.Resources.pd;
                        label1.Text = timer.Elapsed.ToString();
                        Update();
                        System.Threading.Thread.Sleep(80);
                        if (current.komsular[i] != null && current.komsular[i].ziyaret_edildimi == false)
                        {
                            bfs_list.Add(current.komsular[i]);
                        }
                    }
                    bfs_list.Remove(current);
                    (flowLayoutPanel2.Controls[position] as PictureBox).Image = global::BFS.Properties.Resources.circle;
                    bfs_move(bfs_list[0]);
                }
                else
                {
                    bfs_bulundu = true;
                    (flowLayoutPanel2.Controls[29] as PictureBox).Image = global::BFS.Properties.Resources.pd;
                    timer.Stop();
                    MessageBox.Show("BFS " + bfs_adim.ToString() + " adımda BULUNDU");
                    this.Close();
                }

            }
            else
            {
                (flowLayoutPanel2.Controls[position] as PictureBox).Image = null;
                (flowLayoutPanel2.Controls[position] as PictureBox).Image = global::BFS.Properties.Resources.circle;
                bfs_list.Remove(current);
                bfs_move(bfs_list[0]);
            }
        }

      


    }
}
