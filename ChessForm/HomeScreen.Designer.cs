
using System;

namespace ChessForm
{
    partial class HomeScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeScreen));
            this.lbl_NewGame = new System.Windows.Forms.Label();
            this.lbl_Exit = new System.Windows.Forms.Label();
            this.panel_Menu = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Config = new System.Windows.Forms.Label();
            this.panel_Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_NewGame
            // 
            this.lbl_NewGame.AutoSize = true;
            this.lbl_NewGame.BackColor = System.Drawing.Color.Transparent;
            this.lbl_NewGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_NewGame.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_NewGame.Location = new System.Drawing.Point(0, 0);
            this.lbl_NewGame.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_NewGame.Name = "lbl_NewGame";
            this.lbl_NewGame.Size = new System.Drawing.Size(467, 107);
            this.lbl_NewGame.TabIndex = 0;
            this.lbl_NewGame.Text = "Novo jogo";
            this.lbl_NewGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_NewGame.Click += new System.EventHandler(this.lbl_NewGame_Click);
            this.lbl_NewGame.MouseEnter += new System.EventHandler(this.Label_MouseEnter);
            this.lbl_NewGame.MouseLeave += new System.EventHandler(this.Label_MouseLeave);
            // 
            // lbl_Exit
            // 
            this.lbl_Exit.AutoSize = true;
            this.lbl_Exit.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Exit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Exit.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_Exit.Location = new System.Drawing.Point(0, 214);
            this.lbl_Exit.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Exit.Name = "lbl_Exit";
            this.lbl_Exit.Size = new System.Drawing.Size(467, 107);
            this.lbl_Exit.TabIndex = 0;
            this.lbl_Exit.Text = "Sair";
            this.lbl_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Exit.Click += new System.EventHandler(this.lbl_Exit_Click);
            this.lbl_Exit.MouseEnter += new System.EventHandler(this.Label_MouseEnter);
            this.lbl_Exit.MouseLeave += new System.EventHandler(this.Label_MouseLeave);
            // 
            // panel_Menu
            // 
            this.panel_Menu.ColumnCount = 1;
            this.panel_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel_Menu.Controls.Add(this.lbl_Exit, 0, 2);
            this.panel_Menu.Controls.Add(this.lbl_Config, 0, 1);
            this.panel_Menu.Controls.Add(this.lbl_NewGame, 0, 0);
            this.panel_Menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Menu.Location = new System.Drawing.Point(0, 0);
            this.panel_Menu.Name = "panel_Menu";
            this.panel_Menu.RowCount = 3;
            this.panel_Menu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panel_Menu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panel_Menu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panel_Menu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel_Menu.Size = new System.Drawing.Size(467, 321);
            this.panel_Menu.TabIndex = 1;
            // 
            // lbl_Config
            // 
            this.lbl_Config.AutoSize = true;
            this.lbl_Config.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Config.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_Config.Location = new System.Drawing.Point(0, 107);
            this.lbl_Config.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Config.Name = "lbl_Config";
            this.lbl_Config.Size = new System.Drawing.Size(467, 107);
            this.lbl_Config.TabIndex = 0;
            this.lbl_Config.Text = "Configurar";
            this.lbl_Config.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Config.Click += new System.EventHandler(this.lbl_Config_Click);
            this.lbl_Config.MouseEnter += new System.EventHandler(this.Label_MouseEnter);
            this.lbl_Config.MouseLeave += new System.EventHandler(this.Label_MouseLeave);
            // 
            // HomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(467, 321);
            this.Controls.Add(this.panel_Menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "HomeScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.TopMost = true;
            this.panel_Menu.ResumeLayout(false);
            this.panel_Menu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_NewGame;
        private System.Windows.Forms.Label lbl_Exit;
        private System.Windows.Forms.TableLayoutPanel panel_Menu;
        private System.Windows.Forms.Label lbl_Config;
    }
}