   M S B A M L   `   `   `  ????J  FWpfUITestingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ClashOptimiser.UIClashOptimiser.UI  5      5    /CreateRevitSchedule.UI.CreateRevitScheduleUIWPF   5      a ]DevExpress.Xpf.Core.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:ClashOptimiser.UI     5      j fDevExpress.Xpf.LayoutControl.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5	      f bDevExpress.Xpf.Grid.v22.2.Core, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5
      $	??850??5   ?   $	??900??6?     ? Title$  Option??6?   .??5      ?  ? RowDefinitions 5   
   ?? 5      $	??32* ?6   ?? 5      $
??235* ?6   .+?5   
   +  %DevExpress.Xpf.Core.UniformStackPanel  $??0,0,0,21q?6   ?? 5   
   $
??Left=?6   $	??Top=?6+   $$??/Resources/PinnacleWPFLogo.ico??6C   $??-1,0,0,0q?6k   $	??116??6}   ?? 5   
   $$??Create Revit Schedule : Option??6   $??314,17,314,21q?6:   +  %DevExpress.Xpf.LayoutControl.GroupBox  5   
   4  .DevExpress.Xpf.Core.HeaderedContentControlBase   Header$ Select Fields??6   $
??Left=?6/   $??	10,48,0,0q?6J   $	??Top=?6]   $	??615??6u   $	??285??6?   $??2??6?   (  "DevExpress.Xpf.Editors.ListBoxEdit  5   
   -   5          Name$ listBox_categories??5   M   $
??Left=?6   $	??Top=?65   $??	27,95,0,0q?6i   $	??243??6|   $	??227??6?   $??2??6?   5        5   
   -   5        $ listBox_Properties??5   M   $
??Left=?6   $	??Top=?65   $??
27,256,0,0q?6i   $	??243??6}   $??1??6?   $	??285??6?   5      ?? 5   
   $??
Categories??6   $
??Left=?6&   $??	22,79,0,0q?6A   $	??Top=?6T   ?? 5   
   $??Fields??6   $
??Left=?6"   $??
27,227,0,0q?6=   $	??Top=?6Q   $??1??6i    J? RenderTransformOrigin$ 0.664,2.637&?6v   ,  &DevExpress.Xpf.Editors.ProgressBarEdit  5   
   -   5          Name$ progressBar1??5      $??Center=?64   $??	0,581,0,0q?6Q   $??1??6d   $	??Top=?6q   $	??880??6?   &   DevExpress.Xpf.Core.SimpleButton  5   
   -   5          Name$ button_ExportMultiple??5      $??Export Multiple??69   $
??Left=?6S   $??25??6n   $??
10,618,0,0q?6z   $??1??6?   $	??Top=?6?   $	??115??6?     5   
   -   5        $ button_show??5      $
??Show??6/   $
??Left=?6>   $??25??6Y   $??536,618,0,0q?6e   $??1??6z   $	??Top=?6?   $??97??6?     5   
   -   5        $ button_export??5      $??Export to Spreadsheet??61   $??Right=?6Q   $??25??6m   $??
0,618,10,0q?6y   $??1??6?   $	??Top=?6?   $	??143??6?     5    
   ???	#FF0F0F4E$??	0,661,0,0q?66   $??1??6I   ?? 5!   
   $)??#Pinnacle Infotech Solutions - R & D??6   
???????$??Center=?6V   $??	0,677,0,0q?6s   $??1??6?   $	??Top=?6?   ?? 5"   
   $$??Create Revit Schedule - v1.0.0??6   
???????$??Center=?6M   $??	0,699,0,0q?6j   $??1??6}   $	??Top=?6?     5#   
   $
??Left=?6   $??
314,50,0,0q?63   $	??Top=?6G   $	??613??6_   $??2??6l   $	??555??6}   $ Schedule??6?   ,  &DevExpress.Xpf.Core.ContentControlBase   Content. 5$      %  DevExpress.Xpf.Grid.GridControl  )	  #DevExpress.Xpf.Grid.DataControlBase 	  AutoGenerateColumns$ AddNew=?6   #	 	  EnableSmartColumnsGeneration	 . $??0,0,0,15q?6_   
   View
 5%      #
  DevExpress.Xpf.Grid.TableView
  5&      5(      5)      5*      5+      