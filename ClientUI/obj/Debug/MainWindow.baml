   M S B A M L   `   `   `  ����  ClientUI    ClientUI.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   >d2http://schemas.microsoft.com/expression/blend/2008    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � 
ResizeMode$  NoResize=�5   	    � WindowStyle$ 
ToolWindow=�5   	    � Title$ ClientsConsole��5
   	   $��657.966��5   	   $��1236.8��6   .��5      � .+�5      �� -   5        $��listv��5       K� ZIndex$ 1��6   $��Center=�6+   $��Center=�6M   $��21(�6q   $��Times new roman)�6   $
��Left=�6�   $	��413��6�   $��
48,172,0,0q�6�   $	��Top=�6�   $	��579��6�    �� View 5      �� 5      .P�5      ��  �� Width$	 185��6   $��LocalEndPoint��6"    �� DisplayMemberBinding 69   �� *ClientLocalEndPoint+�� 5      $	 190��6   $��RemoteEndPoint��6"    6:   �� *ClientRemoteEndPoint+�� 5      $	 190��6   $��ConnectingTime��6"    6:   �� *ClientConnectingTime+5      5      �� 5      -   5        $��tblock��5      " �� VerticalScrollBarVisibility$ Visible=�6   $ �� HorizontalScrollBarVisibility$ Visible=�6@   $��16(�6h   $��Times new roman)�6v   ���#3C6368	 �� 	MaxLength$	 0��6�   
 �� TextWrapping$

 Wrap=�6�   $��692,90,67.4,113.8q�6�   �� 5      -   5        $��tboxMessage��5      $ 1��6    �� IsInputMethodEnabled .  $��21(�6Y   $��Times new roman)�6g   $��Center=�6�   $��Center=�6�   $
��Left=�6�   $��44��6�   $��666,541,0,0q�6�   $

 Wrap=�6  $	��Top=�6  $	��326��62  �� 5      -   5        $��btnSend��5   4   $ 1��6   $��21(�6C   $��Times new roman)�6Q   $
��Send��6n   $
��Left=�6}   $��997,541,0,0q�6�   $	��Top=�6�   $	��186��6�   $��44��6�   �� 5      -   5        $��tboxEndPoint��5   3   $��127.0.0.1:6000��6   $ 1��6"    .  $��21(�6p   $��Times new roman)�6~   $��Center=�6�   $��Center=�6�   $
��Left=�6�   $��48��6�   $��
76,102,0,0q�6  $

 Wrap=�6  $	��Top=�60  $	��235��6H  �� 5       $ 1��6
   $��21(�6   $��Times new roman)�6)   $��Bottom=�6F   $��From the server:��6h   $
��Left=�6�   $��
692,55,0,0q�6�   $	��Top=�6�   $��30��6�   $	��235��6�   �� 5!      $ 1��6
   $��21(�6   $��Times new roman)�6)   $��Center=�6F   $��Targer EndPoint:��6h   $
��Left=�6�   $��	76,55,0,0q�6�   $	��Top=�6�   $	��235��6�   $��39��6�   �� 5"      -   5        $��btnDisconnect��5"   4   $ 1��6   $��21(�6I   $��Times new roman)�6W   $��
Disconnect��6t   $
��Left=�6�   $��479,102,0,0q�6�   $	��Top=�6�   $	��110��6�   $��48��6�   �� 5#      -   5        $��
btnConnect��5#   2   $ 1��6   $��21(�6D   $��Times new roman)�6R   $��Connect��6o   $
��Left=�6�   $��349,102,0,0q�6�   $	��Top=�6�   $	��100��6�   $��48��6�   �� 5$      $ 0��6   $��Communication��6   $��16(�65   $��Times new roman)�6C   $	��506��6`   $	��Top=�6m   $��666,30,47.4,0q�6�   �� 5%      $ 0��6   $��
Connection��6   $��16(�62   $��Times new roman)�6@   $	��131��6]   $	��Top=�6j   $��48,30,603.4,0q�6�   ]� 5&      .+�5'      o� $	��Top=�6
   $
��Left=�6   $��24��6:   $	��Top=�6F   $	��994��6^   $��
0,0,-0.4,0q�6j   .@�5(      m� -	   5        $��Query��5(      $��16(�68   $��Times new roman)�6F   $��24��6c   5)      5*      5+      5,      