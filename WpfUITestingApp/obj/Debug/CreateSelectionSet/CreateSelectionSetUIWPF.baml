   M S B A M L   `   `   `  ????J  FWpfUITestingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ClashOptimiser.UIClashOptimiser.UI  5      3    -CreateSelectionSet.UI.CreateSelectionSetUIWPF   5      a ]DevExpress.Xpf.Core.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:ClashOptimiser.UI     5      j fDevExpress.Xpf.LayoutControl.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5	        ? Title$  Create Selection Set??5      $	??390??6?   $	??450??6?   .??5
      ?  ? RowDefinitions 5   
   ?? 5      $	??96* ?6   .+?5   
   ?? $??Tab Name : ??6   $??10,10,322,297q?6&   ?? 5   
   $)??#Pinnacle Infotech Solutions - R & D??6   
???????$
??Left=?6U   $??255,220,0,0q?6p   $	??Top=?6?   $??1??6?   ?? 5   
   $$??Create Revit Schedule - v1.0.0??6   
???????$
??Left=?6L   $??269,242,0,0q?6g   $	??Top=?6|   $??1??6?   %  DevExpress.Xpf.Editors.TextEdit  5   
   -   5          Name$ txtCategoryName??5      $??Center=?60   $??0,42,0,0q?6M   $	??Top=?6_   $??34??6w   $	??380??6?   ?? 5   
   $??OR??6   $??176,74,200,233q?6    J? RenderTransformOrigin$ 
1.24,0.584&?65   )  #DevExpress.Xpf.Editors.ComboBoxEdit  5   
   -   5          Name$ 
cmbTabName??5      $??Center=?6/   $??	0,100,0,0q?6L   $	??Top=?6_   $??38??6w   $	??380??6?   ?? 5   
   $??Property Name : ??6   $??10,154,298,153q?6+     5   
   -   5        $ txtPropertyName??5      $??Center=?60   $??	0,254,0,0q?6M   $	??Top=?6`   $??35??6x   $	??380??6?   ?? 5   
   $??OR??6   $??176,221,200,86q?6   $ 
1.24,0.584&?65     5   
   -   5        $ cmbPropertyName??5      $??Center=?64   $??	0,181,0,0q?6Q   $	??Top=?6d   $??38??6|   $	??380??6?   &   DevExpress.Xpf.Core.SimpleButton  5   
   -   5          Name$ CreateSelectionSetButton??5      $??Create Selection Sets??6<   $??Right=?6\   $??34??6x   $??
0,304,27,0q?6?   $	??Top=?6?   $	??156??6?   5      5      