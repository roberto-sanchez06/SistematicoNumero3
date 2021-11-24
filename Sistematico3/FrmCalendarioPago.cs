﻿using AppCore.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistematico3
{
 
    public partial class FrmCalendarioPago : Form
    {
        public ICalendarioService calendarioS { get; set; }
        public int Id { get; set; }
        public FrmCalendarioPago()
        {
            InitializeComponent();
        }
        private void llenarDtgv()
        {
            Calendario c =calendarioS.FindAll().Find(x=>x.Id==Id);
            if (c == null)
            {
                MessageBox.Show("No se ha encontrado el prestamo");
                return;
            }
            DateTime Fecha = c.FechaPago;

            decimal interes, pagoPrinci, saldo = c.Principal;
            for (int i=0; i < c.Terminos; i++)
            {
                interes = saldo * (c.Tasa / 100);
                pagoPrinci = c.Cuota - interes;
                dataGridView1.Rows.Add(i+1, Fecha.AddMonths(i), interes,c.Cuota,pagoPrinci, saldo);
                saldo -= pagoPrinci;
            }
        }

        private void FrmCalendarioPago_Load(object sender, EventArgs e)
        {
            llenarDtgv();
        }
    }
}
