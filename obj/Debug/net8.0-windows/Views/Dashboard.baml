   M S B A M L   `   `   `  ����>  :Fit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Fit.Views	Fit.Views  5          Fit.Views.Dashboard   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      )localclr-namespace:Fit.Views     5      -   5          N�Name$  DashboardControl��5      $
��Auto��5      $
��Auto��5      .��5	      � ���#F0F0F0 � RowDefinitions 5
   
   �� 5      $��60 �6   �� 5      $��* �6   .+�5   
      Fit.Views.Topbar  $��0��6   �� 5   
   $��1��6   $��20,20,20,20q�6$   $
��Auto=�69   .��5      �  5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $��* �6$   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   $��0��6S   .+�5      �� ���#FFD700 �� CornerRadius$ 15��62   $��10q�6D    �� Padding$ 20q�6P   $	��200��6]   ._�5       �� .+�5!      �� $��Total Calories Burned��6(   $��12(�6E   $
��Bold"�6S   $��0,0,0,10q�6e   
���   �6w   �� 5"      -   5         ��Name$ CaloriesBurnedTextBlock��5"   (   $��Total Calories Burned: 0��6I   $��14(�6i   $
��Bold"�6w   
���   �6�   5#      5$      �� 5&      ���#FFDDC1$ 15��62   $��10q�6D   $ 20q�6P   $	��200��6]   ._�5'      �� .+�5(      �� $��Daily Goal Suggestion��6(   $��12(�6E   $
��Bold"�6S   $��0,0,0,10q�6e   
���   �6w   �� 5)      $��Aim for 10 goals today!��6(   $��12(�6G   
���   �6U   5*      5+      �� 5-      ���#B2E0FF$ 15��62   $��10q�6D   $ 20q�6P   $	��200��6]   ._�5.      �� .+�5/      �� $��Activity Suggestion��6(   $��12(�6C   $
��Bold"�6Q   $��0,0,0,10q�6c   
���   �6u   �� 50      $"��Complete 5 activities today!��6(   $��12(�6L   
���   �6Z   51      52      �� 54      ���#FFCCCB$ 15��62   $��10q�6D   $ 20q�6P   $	��200��6]   ._�55      �� .+�56      �� $��Steps Taken Suggestion��6(   $��12(�6F   $
��Bold"�6T   $��0,0,0,10q�6f   
���   �6x   �� 57      $!��Aim for 10,000 steps today!��6(   $��12(�6K   
���   �6Y   58      59      �� 5;      ���#D1E7DD$ 15��62   $��10q�6D   $ 20q�6P   $	��200��6]   ._�5<      �� .+�5=      �� $!��Workout Duration Suggestion��6(   $��12(�6K   $
��Bold"�6Y   $��0,0,0,10q�6k   
���   �6}   �� 5>      $*��$Try to work out for at least 30 min!��6(   $��12(�6T   
���   �6b   5?      5@      �� 5B      ���#E2E3E5$ 15��62   $��10q�6D   $ 20q�6P   $	��200��6]   ._�5C      �� .+�5D      �� $��Water Intake Suggestion��6(   $��12(�6G   $
��Bold"�6U   $��0,0,0,10q�6g   
���   �6y   �� 5E      $'��!Drink at least 2 liters of water!��6(   $��12(�6Q   
���   �6_   5F      5G      5H      �� 5J      $��1��6   $
��Auto=�6,   .��5K      �� .+�5L      �� $��
Your Goals��6$   $��18(�66   $
��Bold"�6D   $��0,0,0,10q�6V   &   System.Windows.Controls.DataGrid  5M      -   5        $��GoalsDataGrid��5M   #      AutoGenerateColumns$ False��68      CanUserAddRows$ False��6T      HeadersVisibility$ Column=�6k   	   Columns	 5N      0  *System.Windows.Controls.DataGridTextColumn  5O   "   ,  &System.Windows.Controls.DataGridColumn
   Header$
 Activity��65      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6`   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *Name+6`   4  .System.Windows.Controls.DataGridTemplateColumn  5P   "   $
 Description��69   $ * 6N      CellTemplate 5Q   &   �� 5R   *   .R�5S   .   �� $
��Wrap=�6U   ��68   �� *Description+6U   5T   +   5V   #     5W   "   $
 Id��65   $ * 6X    6A   �� *Id+6X     5X   "   $
 Calorie Target��65   $ * 6o    6M   �� *CalorieTarget+6o     5Y   "   $
 
Start Date��65   $ * 6�    6I   �� *	StartDate+ �� StringFormat$ {0:MM/dd/yyyy}��6�     5Z   "   $
 End Date��65   $ * 6�    6G   �� *	EndDate+$ {0:MM/dd/yyyy}��6�   5\      5^      5_      5`      5a      5b      5c      