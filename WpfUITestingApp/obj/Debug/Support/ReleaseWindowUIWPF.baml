   M S B A M L   `   `   `  ????J  FWpfUITestingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ClashOptimiser.UIClashOptimiser.UI  5      #    Support.UI.ReleaseWindowUIWPF   5      a ]DevExpress.Xpf.Core.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:ClashOptimiser.UI     5      j fDevExpress.Xpf.LayoutControl.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5	      f bDevExpress.Xpf.Grid.v22.2.Core, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v22.2, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5
      $	??550??5   t   $	??600??6?     ? Title$  	Main Page??6?   .??5      ?  ? ColumnDefinitions 5   
   ?? 5      $
??152* ?6   ?? 5      $
??141* ?6   ?? 5      $??7* ?6    ? RowDefinitions 5   
   ?? 5      $	??32* ?6   .+?5   
   +  %DevExpress.Xpf.Core.UniformStackPanel  $??	0,0,0,465q?6   $??3??62   ?? 5   
   $
??Left=?6   $	??Top=?6+   $$??/Resources/PinnacleWPFLogo.ico??6C   $??-1,0,0,0q?6k   $	??116??6}   ?? 5   
   $$??Plugins Information : MainPage??6   $??200,17,186,465q?6:   $??2??6R   &   DevExpress.Xpf.Core.SimpleButton  5   
   -   5          Name$ button_LogIn??5      $??Login??60   $
??Left=?6@   $??25??6[   $??
10,408,0,0q?6g   $	??Top=?6{   $	??115??6?     5   
   -   5        $ 	btn_Reset??5      $??Reset??6-   $
??Left=?6=   $??26??6X   $??153,408,0,0q?6d   $	??Top=?6y   $??97??6?    J? RenderTransformOrigin$ 0.361,-0.306&?6?     5   
   -   5        $ button_PrjInfo??5      $??Provide ProjectInfo??62   $
??Left=?6P   $??25??6k   $??123,408,0,0q?6w   $	??Top=?6?   $	??143??6?   $??1??6?     5   
   ???	#FF0F0F4E$??
0,451,0,-2q?66   $??3??6J   ?? 5   
   $)??#Pinnacle Infotech Solutions - R & D??6   
???????$
??Left=?6V   $??207,467,0,0q?6q   $	??Top=?6?   $??22??6?   $??2??6?   ?? 5   
   $$??Create Revit Schedule - v1.0.0??6   
???????$
??Left=?6M   $??222,489,0,0q?6h   $	??Top=?6}   $??24??6?   $??2??6?   %  DevExpress.Xpf.Grid.GridControl  5   
   -   5          Name$ dataGridView1??5      )  #DevExpress.Xpf.Grid.DataControlBase   AutoGenerateColumns$ AddNew=?61   #   EnableSmartColumnsGeneration . $??10,69,10,131q?6r   $??3??6?   5       5!      5"      