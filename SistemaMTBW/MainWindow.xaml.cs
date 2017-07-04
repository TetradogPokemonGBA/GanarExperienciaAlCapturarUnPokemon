﻿using Gabriel.Cat;
using Gabriel.Cat.Extension;
using Microsoft.Win32;
using PokemonGBAFrameWork;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaMTBW
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RomGba rom;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Esta aplicación pone el sistema de MT de Blanco y Negro que simplemente hace que no se gasten las MT cuando se usen.\n\nBug conocido: solo se gastan si se cancela la animación de aprender la MT\n\nDesarrollado por Pikachu240 investigado por FBI y BLAx501! ","Sobre la App");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opnRom = new OpenFileDialog();
            opnRom.Filter = "Rom Pokemon GBA|*.gba";
            try
            {
                if (opnRom.ShowDialog().GetValueOrDefault())
                {
                    rom = new RomGba(opnRom.FileName);
                    PonTexto();
                    btnPonerOQuitar.IsEnabled = true;
                    switch(EdicionPokemon.GetEdicionPokemon(rom).AbreviacionRom)
                    {
                        case AbreviacionCanon.BPE:imgDecoración.SetImage(Imagenes.PokeballEsmeralda);break;
                        case AbreviacionCanon.BPR: imgDecoración.SetImage(Imagenes.PokeballRojoFuego); break;
                        case AbreviacionCanon.BPG: imgDecoración.SetImage(Imagenes.PokeballVerdeHoja); break;
                        case AbreviacionCanon.AXV: imgDecoración.SetImage(Imagenes.PokeballRuby); break;
                        case AbreviacionCanon.AXP: imgDecoración.SetImage(Imagenes.PokeballZafiro); break;
                    }
                }
                else if(rom!=null)
                {
                    MessageBox.Show("No se ha cambiado la rom","Atención",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }else
                {
                    MessageBox.Show("No se ha cargado nada...");
                }
            }catch
            {
                btnPonerOQuitar.IsEnabled = false;
                rom = null;
                imgDecoración.SetImage(new Bitmap(1, 1));
                MessageBox.Show("La rom no es compatible","Aun no es universal...");
            }
        }

        private void PonTexto()
        {
           if(PokemonGBAFrameWork.SistemaMTBW.EstaActivado(rom, EdicionPokemon.GetEdicionPokemon(rom), Compilacion.GetCompilacion(rom,EdicionPokemon.GetEdicionPokemon(rom))))
            {
                btnPonerOQuitar.Content = "Volver al sistema anterior";
            }
           else
            {
                btnPonerOQuitar.Content = "Poner Sistema MT BW!";
            }
        }

        private void btnPonerOQuitar_Click(object sender, RoutedEventArgs e)
        {
        	if (PokemonGBAFrameWork.SistemaMTBW.EstaActivado(rom, EdicionPokemon.GetEdicionPokemon(rom), Compilacion.GetCompilacion(rom,EdicionPokemon.GetEdicionPokemon(rom))))
            {
        	    	PokemonGBAFrameWork.SistemaMTBW.Desactivar(rom, EdicionPokemon.GetEdicionPokemon(rom), Compilacion.GetCompilacion(rom,EdicionPokemon.GetEdicionPokemon(rom)));
             
            }
            else
            {
            	PokemonGBAFrameWork.SistemaMTBW.Activar(rom, EdicionPokemon.GetEdicionPokemon(rom), Compilacion.GetCompilacion(rom,EdicionPokemon.GetEdicionPokemon(rom)));
   
            }
            PonTexto();
            rom.Save();
        }
    }
}
