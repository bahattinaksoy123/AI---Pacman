using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sinirli_derinlikte_arama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool bulundu = false;
        int dfs_limit_adim = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            try {
                limit = Convert.ToInt32(txt_limit.Text);
                button2.Visible = false;
                matrisx_doldur();
                flowlayoutpanel_doldur();
                dfs_olustur();
                timer.Start();
                dfs_limit_move(29, 0);
                if (!bulundu) MessageBox.Show("Bu limitte aramada bulunamadı");

            } catch { }
           
        }

        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

        private void dfs_limit_move(int p_position, int seviye)
        {
            if (seviye <= limit)
            {
                if (p_position != 405)
                {
                    try
                    {
                        dfs_list[p_position].ziyaret_edildimi = true;
                        for (int i = 0; i < 4; i++)
                        {

                            if ((flowLayoutPanel2.Controls[p_position] as PictureBox).Image != global::Sinirli_derinlikte_arama.Properties.Resources.pd) { dfs_limit_adim++; }
                            (flowLayoutPanel2.Controls[p_position] as PictureBox).Image = global::Sinirli_derinlikte_arama.Properties.Resources.pd;
                            label2.Text = timer.Elapsed.ToString();
                            Update();
                            System.Threading.Thread.Sleep(80);
                            (flowLayoutPanel2.Controls[p_position] as PictureBox).Image = null;
                            (flowLayoutPanel2.Controls[p_position] as PictureBox).BackgroundImage = global::Sinirli_derinlikte_arama.Properties.Resources.circle;
                            if (dfs_list[p_position].komsular[i] != null && dfs_list[p_position].komsular[i].ziyaret_edildimi == false)
                            {
                                int p_position_now = dfs_list[p_position].komsular[i].x * 28 + dfs_list[p_position].komsular[i].y;
                                dfs_limit_move(p_position_now, (seviye + 1));
                            }
                        }
                    }
                    catch { }
                }
                else
                {
                    (flowLayoutPanel2.Controls[p_position] as PictureBox).Image = global::Sinirli_derinlikte_arama.Properties.Resources.pd;
                    timer.Stop();
                    MessageBox.Show("Sınırlı derinlikte arama " + (dfs_limit_adim / 2).ToString() + " adımda BULUNDU");
                    bulundu = true;
                    this.Close();
                }
            }
        }
            int[,] matrisx;
            int limit = 33;
        class node_dfs
        {
            public int x = 0;
            public int y = 0;
            public bool ziyaret_edildimi = false;
            public node_dfs[] komsular = new node_dfs[4] { null, null, null, null };
        }
        node_dfs[] dfs_list;

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
            (flowLayoutPanel2.Controls[29] as PictureBox).Image = global::Sinirli_derinlikte_arama.Properties.Resources.pd;
            //ilk yiyecegi yerlestir//
            (flowLayoutPanel2.Controls[405] as PictureBox).Image = global::Sinirli_derinlikte_arama.Properties.Resources.strawberry;
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






    }

}

