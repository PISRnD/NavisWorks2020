   M S B A M L   `   `   `  ????J  FWpfUITestingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ClashOptimiser.UIClashOptimiser.UI  5      A    ;LevelwiseViewpointCreater.UI.LevelwiseViewpointCreaterUIWPF   5      a ]DevExpress.Xpf.Core.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:ClashOptimiser.UI     5      j fDevExpress.Xpf.LayoutControl.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5	      f bDevExpress.Xpf.Grid.v22.2.Core, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5
        ? Title$  View Point Exporter??5      $	??380??6?   $	??520??6?   .??5      ?  ? RowDefinitions 5   
   ?? 5      $	??63* ?6   .+?5   
   %  DevExpress.Xpf.Editors.TextEdit  )  #DevExpress.Xpf.Editors.TextEditBase   Text$ Export Location : ??6   $??10,243,377,66q?61     5   
   $) #Pinnacle Infotech Solutions - R & D??6   
???????$
??Left=?6Y   $??255,157,0,0q?6t   $	??Top=?6?   $??1??6?     5   
   $$ Create Revit Schedule - v1.0.0??6   
???????$
??Left=?6P   $??269,179,0,0q?6k   $	??Top=?6?   $??1??6?   &   DevExpress.Xpf.Core.SimpleButton  5   
   -   5          Name$ 	btnExport??5      $??Export??6-   $??Right=?6>   $??34??6Z   $??
0,251,10,0q?6f   $	??Top=?6z   $??69??6?   )  #DevExpress.Xpf.Grid.TreeViewControl  5   
   -   5          Name$ treeViewPointList??5      $??Center=?69   $	??220??6V   $??0,10,0,0q?6c   $	??Top=?6u   $	??480??6?     5   
   -   5        $ 	btnImport??5      $??Import??6-   $??Right=?6>   $??34??6Z   $??
0,290,10,0q?6f   $	??Top=?6z   $??69??6?     5   
   -   5          Name$ txtExportLoc??5      $
??Left=?6-   $??
10,281,0,0q?6H   $	??Top=?6\   $	??406??6t   $??40??6?   5      5      