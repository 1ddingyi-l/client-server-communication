   M S B A M L   `   `   `  ����  bg    ReformCodes.ServerUI   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � WindowStyle$  
ToolWindow=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5	   	    � Title$ ServersConsole��5
   	   $��21(�5   	   $��Times new roman)�5   	   $	��600��5   	   $	��850��6   .��5      � .+�5   
   �� -   5        $��btnAddServer��5   4    K� ZIndex$ 1��6   $��
Add server��6H   $��21(�6]   $��Times new roman)�6k   $
��Left=�6�   $��529,158,0,0q�6�   $	��Top=�6�   $	��206��6�   $��39��6�   �� 5   
   -   5        $��tboxServerPort��5   /   $
��6000��6   $ 1��6   $��21(�6E   $��Times new roman)�6S   $��Center=�6p   $��Center=�6�   $
��Left=�6�   $��39��6�   $��
529,97,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6  $	��206��6  �� 5   
   -   5        $��btnCloseServer��5   7   $ 1��6   $��Close server��6M   $��21(�6d   $��Times new roman)�6r   $
��Left=�6�   $��529,225,0,0q�6�   $	��Top=�6�   $	��206��6�   $��39��6�   �� 5   
   -   5        $��btnCloseConnection��5   :   $ 1��6   $��Close connection��6T   $��21(�6o   $��Times new roman)�6}   $
��Left=�6�   $��529,384,0,0q�6�   $	��Top=�6�   $	��206��6�   $��39��6�   �� 5   
   -   5        $��serverListView��5   ]   $ 1��6+   $
��Left=�6s   $��Center=�6�   $��Center=�6�   $	��200��6�   $��	82,79,0,0q�6�   $	��Top=�6�   $	��373��6  ��6   ��  �� View 5      �� 5      .P�5      �� $��ServerEndPoint��6%    �� Width$	 150��6=    �� DisplayMemberBinding 6I   �� *ServerIPEndPoint+�� 5      $��StartingTime��6%   $	 220��6;    6G   �� *ServerStartingTime+5      5      �� 5   
   -   5        $��clientListView��5   <   $ 1��6+   $
��Left=�6R   $��Center=�6m   $	��182��6�   $��
82,348,0,0q�6�   $	��Top=�6�   $	��373��6�   ��6   ��  5      �� 5      .P�5      �� $��ClientEndPoint��6%   $	 150��6r    6=   �� *ClientLocalEndPoint+6r   �� 5       $��ConnectedTime��6%   $	 220��6r    6<   �� *ClientConnectingTime+6r   5!      5#      �� 5$   
   -   5        $ 1��5$      $��Refresh��62   $
��Left=�6D   $��529,456,0,0q�6_   $	��Top=�6t   $	��206��6�   $��39��6�   �� 5%   
   $ 0��6   $'��!Corresponding client connections:��6$   $
��Left=�6O   $	��240��6j   $��
60,307,0,0q�6w   $	��Top=�6�   $	��418��6�   �� 5&   
   $ 0��6   $��Servers running:��6$   $
��Left=�6>   $	��266��6Y   $��	60,36,0,0q�6f   $	��Top=�6y   $	��418��6�   �� 5'   
   $ 0��6   $��Server operations��6$   $
��Left=�6?   $	��266��6Z   $��
496,36,0,0q�6g   $	��Top=�6{   $	��258��6�   �� 5(   
   $ 0��6   $��Client operations��6$   $
��Left=�6?   $	��240��6Z   $��496,307,0,0q�6g   $	��Top=�6|   $	��258��6�   5*      5+      