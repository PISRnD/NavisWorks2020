   M S B A M L   `   `   `  ????J  FWpfUITestingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ClashOptimiser.UIClashOptimiser.UI  5      4    .MergeTimeLinerData.UI.NavisworksTimelinerUIWPF   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:ClashOptimiser.UI     5      j fDevExpress.Xpf.LayoutControl.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5	      f bDevExpress.Xpf.Grid.v22.2.Core, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5
        ? Title$6  0Timeline Data Merger | Navisworks Package v3.0.0??5      $	??500??5      $	??620??6   .??5      ?  ? RowDefinitions 5   
   ?? 5      $	??63* ?6   .+?5   
   +  %DevExpress.Xpf.LayoutControl.GroupBox  4  .DevExpress.Xpf.Core.HeaderedContentControlBase   Header$ Export File Selection??6   $
??Left=?67   $??	16,10,0,0q?6R   $	??Top=?6e   $	??331??6}   $	??253??6?     5   
   $ Export Appearance Part??6   $
??Left=?68   $??	26,58,0,0q?6S   $	??Top=?6f   $??80??6~   $	??229??6?   ,  &DevExpress.Xpf.Core.ContentControlBase   Content. 5      &   DevExpress.Xpf.Core.SimpleButton  -   5          Name$ button1??5      $??Start Process??6/   $
??Left=?6G   $	??Top=?6b   $??30??6z   $	??203??6?   5        5   
   $ Export Configure Part??6   $
??Left=?67   $??
28,152,0,0q?6R   $	??Top=?6f   $??80??6~   $	??229??6?   . 5        -   5        $ button6??5      $??Start Process??6/   $
??Left=?6G   $	??Top=?6b   $??30??6z   $	??203??6?   5        5   
   $ Export Task Part??6   $
??Left=?62   $??
26,248,0,0q?6M   $	??Top=?6a   $??78??6y   $	??229??6?   . 5        -   5        $ button7??5      $??Start Process??6/   $
??Left=?6G   $	??Top=?6b   $??30??6z   $	??203??6?   5        5   
   $ Import File Selection??6   $
??Left=?67   $??
317,10,0,0q?6R   $	??Top=?6f   $	??331??6~   $	??253??6?   ?? 5    
   $??Import Appearance Part??6   $
??Left=?63   $??
329,58,0,0q?6N   $	??Top=?6b   $??80??6z   $	??229??6?   .??5!        -   5        $ button2??5!      $??Choose File??6/   $
??Left=?6E   $	??Top=?6`   $??30??6x   $	??203??6?   5"        5#   
   $ Import Configure Part??6   $
??Left=?67   $??329,160,0,0q?6R   $	??Top=?6g   $??80??6   $	??229??6?   . 5$        -   5        $ button4??5$      $??Choose File??6/   $
??Left=?6E   $	??Top=?6`   $??30??6x   $	??203??6?   5%        5&   
   $ Import Task Part??6   $
??Left=?62   $??329,248,0,0q?6M   $	??Top=?6b   $??78??6z   $	??229??6?   . 5'        -   5        $ button3??5'      $??Choose File??6/   $
??Left=?6E   $	??Top=?6`   $??30??6x   $	??203??6?   5(        5)   
   $ 	Operation??6   $
??Left=?6+   $??170,364,0,0q?6F   $	??Top=?6[   $??78??6s   $	??229??6   . 5*        -   5        $ button5??5*      $
??Exit??6/   $
??Left=?6>   $	??Top=?6Y   $??30??6q   $	??203??6}   5+      5,      5-      