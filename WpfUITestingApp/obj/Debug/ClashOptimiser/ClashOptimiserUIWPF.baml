   M S B A M L   `   `   `  ????J  FWpfUITestingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ClashOptimiser.UIClashOptimiser.UI  5      +    %ClashOptimiser.UI.ClashOptimiserUIWPF   5      a ]DevExpress.Xpf.Core.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  f bDevExpress.Xpf.Grid.v22.2.Core, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:ClashOptimiser.UI     5      j fDevExpress.Xpf.LayoutControl.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5	      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5
      d	 `DevExpress.Xpf.Docking.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aQdxdoveDhttp://schemas.devexpress.com/winfx/2008/xaml/docking/visualelements 	 5      c
 _DevExpress.Xpf.Charts.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxc4http://schemas.devexpress.com/winfx/2008/xaml/charts 
 5      -   5        &   DevExpress.Xpf.Core.ThemedWindow   Name$  ClashOptimiserUI??5   F   $	??800??5      $	??820??6    ? Title$' !Clash Optimiser | PiVDCNavisworks??5      .??5      ?  ? RowDefinitions 5   
   ?? 5      $
??529* ?6   ?? 5      $
??195* ?6   .+?5   
   ?? -   5         ??Name$ 	groupBox1??5   ,   $??Available Files??6   $	??Top=?6?   $??Center=?6W   $??0,10,0,0q?6t   $	??116??6?   $	??780??6?   .??5      (  "DevExpress.Xpf.Editors.ListBoxEdit  -   5        $??lstAvailFiles??5   9   $
??Left=?6   $??10,0,0,0q?6N   $	??Top=?6`   $??76??6x   $	??746??6?   5      %  DevExpress.Xpf.Grid.GridControl  5   
   -   5          Name$ dataClashGrid??5      $??10,131,10,218q?61   )  #DevExpress.Xpf.Grid.DataControlBase   AutoGenerateColumns$ AddNew=?6H   #   EnableSmartColumnsGeneration . 6e   ?? 5   
   $??Name :??6   $
??Left=?6"   $??
14,342,0,0q?6=   $	??Top=?6Q   $??22??6i   $??38??6u   ?? 5   
   $??Clash Test Name??6   $
??Left=?6+   $??
73,342,0,0q?6F   $	??Top=?6Z   $??22??6r   $??87??6~   ?? 5   
   $??Tolerence :??6   $
??Left=?6'   $??259,342,0,0q?6B   $	??Top=?6W    J? RenderTransformOrigin$ -0.604,0.233&?6o   $??22??6?   $??56??6?   ?? 5   
   -   5        $ 	groupBox2??5      $??Ignore??6&   $
??Left=?66   $	??Top=?6Q   $??
14,368,0,0q?6i   $??55??6}   $	??350??6?   ???	#000000006?   &   DevExpress.Xpf.Editors.CheckEdit  5   
   -   5          Name$ chkIgnoreReviewed??5      	   Content)  #DevExpress.Xpf.Core.ObjectConverter$	 Reviewed 63   $
??Left=?6F   $??
89,381,0,0q?6a   $	??Top=?6u   $??31??6?   $??69??6?     5    
   -   5        $ chkIgnoreApproved??5       $	 Approved 63   $
??Left=?6F   $??223,381,0,0q?6a   $	??Top=?6v   $??31??6?   $??71??6?   ?? 5!   
   -   5        $ 	groupBox3??5!   &   $??	Tolerence??6   $
??Left=?69   $??436,363,0,0q?6T   $	??Top=?6i   $??60??6?   $	??347??6?   z?5"      V? 5#      .~?5$      ?? ?? 5%      ?? 5&      T? 5'      5(      .??5*      )  #DevExpress.Xpf.Editors.TrackBarEdit  -	   5        
  Name$
 trackTolerence??5*      $??	64,0,51,0q?67   5+      ?? 5,   
   $??Local??6   $
??Left=?6!   $??443,390,0,0q?6<   $	??Top=?6Q   $??22??6i   $??40??6u   ?? 5-   
   $??Global??6   $
??Left=?6"   $??730,399,0,0q?6=   $	??Top=?6R   $ 0.633,0.695&?6j   $??22??6?   $??53??6?   ,  &DevExpress.Xpf.Editors.ProgressBarEdit  5.   
   $
??Left=?6   $??
14,440,0,0q?69   $	??Top=?6M   $	??769??6e   $??38??6q   	? 5/   
   -
   5         	?Name$ rdBtnBoundingBox??5/      $??Bounding Box??60   $
??Left=?6G   $??
19,488,0,0q?6b   $	??Top=?6v   $??39??6?   $	??114??6?   z?50      V? 51      .~?52      ?? ?? 53      ?? 54      T? 55      56      58      	? 59   
   -   5        $ rdBtnCategory??59      $
??Name??6-   $
??Left=?6<   $??147,488,0,0q?6W   $	??Top=?6l   $??39??6?   $??91??6?   z?5:      V? 5;      .~?5<      ?? ?? 5=      ?? 5>      T? 5?      5@      5B        5C   
   -   5        $ 
chkArrange??5C      $	 Arrange 6,   $
??Left=?6>   $??287,485,0,0q?6Y   $	??Top=?6n   $ 0.328,-0.725&?6?   $??32??6?   $??87??6?   &	   DevExpress.Xpf.Core.SimpleButton	  5D   
   -   5         	 Name$ btnReset??5D      $??UNGROUP??6,   $
??Left=?6>   $??22??6Y   $??423,485,0,0q?6e   $	??Top=?6z   $??79??6?   	  5E   
   -   5        $ btnOptimise??5E      $??OPTIMIZE??6/   $
??Left=?6B   $??22??6]   $??606,485,0,0q?6i   $	??Top=?6~   $??79??6?   ?? 5F   
   $??
Tab Name :??6   $
??Left=?6&   $??	36,17,0,0q?6A   $	??Top=?6T   $ 0.506,7.379&?6l   $??22??6?   $??59??6?   $??1??6?   ?? 5G   
   $??Property Name :??6   $
??Left=?6+   $??	36,68,0,0q?6F   $	??Top=?6Y   $??1??6q   $??22??6~   $??86??6?   ?? 5H   
   $??Property Value :??6   $
??Left=?6,   $??
36,110,0,0q?6G   $	??Top=?6[   $??1??6s   $??22??6?   $??84??6?   $ 0.506,0.864&?6?   (
  "DevExpress.Xpf.Core.DropDownButton
  5I   
   -   5         
 Name$ 
cmbTabName??5I      $??DropDownButton??60   $
??Left=?6I   $??140,9,0,156q?6d   $	??146??6y   $??1??6?   z?5J      V? 5K      .~?5L      ?? ?? 5M      ?? 5N      T? 5O      5P      5R      
  5S   
   -   5        $ cmbPropertyName??5S      $??DropDownButton??65   $
??Left=?6N   $??
140,56,0,0q?6i   $??1??6}   $	??Top=?6?   $??34??6?   $	??146??6?   
  5T   
   -   5        $ cmbPropertyValue??5T      $??DropDownButton??66   $
??Left=?6O   $??140,103,0,0q?6j   $??1??6   $	??Top=?6?   $??35??6?   $	??146??6?   ?? 5U   
   $??OR??6   $
??Left=?6   $??
373,61,0,0q?69   $	??Top=?6M   $ 0.506,7.379&?6e   $??22??6?   $??16??6?   $??1??6?   ?? 5V   
   $??OR??6   $
??Left=?6   $??	373,6,0,0q?69   $	??Top=?6L   $ 0.506,7.379&?6d   $??1??6?   $??22??6?   $??16??6?   ?? 5W   
   $??OR??6   $
??Left=?6   $??373,114,0,0q?69   $	??Top=?6N   $ 0.506,7.379&?6f   $??1??6?   $??22??6?   $??16??6?   %  DevExpress.Xpf.Editors.TextEdit  5X   
   $
??Left=?6   $??463,104,0,0q?62   $	??Top=?6G   $	??187??6_   $??32??6k   $??1??6w   z?5Y      V? 5Z      .~?5[      ?? ?? 5\      ?? 5]      T? 5^      5_      5a        5b   
   $
??Left=?6   $??
465,13,0,0q?62   $??1??6F   $	??Top=?6S   $	??187??6k   $??26??6w   z?5c      V? 5d      .~?5e      ?? ?? 5f      ?? 5g      T? 5h      5i      5k        5l   
   -   5          Name$ txtPropertyValue??5l      $
??Left=?61   $??
465,61,0,0q?6L   $??1??6`   $	??Top=?6m   $	??185??6?   $??29??6?   z?5m      V? 5n      .~?5o      ?? ?? 5p      ?? 5q      T? 5r      5s      5u      7 
 1DevExpress.Xpf.Charts.MajorGridLinesRenderControl  5v   
   $
??Left=?6*   $??165,150,0,0q?6E   $	??Top=?6Z   $??0??6r   $??0??6}   5w      5x      