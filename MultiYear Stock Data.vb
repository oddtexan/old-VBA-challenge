Sub WorksheetLoop()

'First we set MainWs as a worksheet object variable
Dim headers() As Variant
Dim MainWs As Worksheet
Dim wb As Workbook

Set wb = ActiveWorkbook

'Next we set header info
headers() = Array("Ticker", "Date", "Open", "High", "Low", "Close", "Volume", " ", "Ticker", "Yearly_Change","Percent_Change","Total_Stock_Volume"," " , " ", " ", "Ticker", "Value" )

For Each MainWs In wb.Sheets
    With MainWs
    .Rows(1).Value = ""
    For i = LBound(headers()) To UBound(headers())
    .Cells(1,1+i).Value = headers(i)

    Next i
    .Rows(1).Font.Bold = True
    .Rows(1).VerticalAlignment = xlCenter
    End With
Next MainWs

' We will now loop through all of the worksheets in the workbook
For Each MainWs In Worksheets

'Next we declare initial variables for calculations
     Dim Ticker_Name as String
     Ticker_Name = " "
     Dim Total_Ticker_Volume As Double 
     Total_Ticker_Volume = 0
     Dim Beg_Price As Double
     Beg_Price = 0 
     Dim End_Price As Double
     End_Price = 0
     Dim Yearly_Price_Change as Double
     Yearly_Price_Change = 0
     Dim Yearly_Price_Change_Percent as Double
     Yearly_Price_Change_Percent = 0
     Dim Max_Ticker_Name as String
     Max_Ticker_Name = " "
     Dim Min_Ticker_Name as String
     Min_Ticker_Name = " "
     Dim Max_Percent As Double
     Max_Percent = 0
     Dim Min_Percent As Double
     Min_Percent = 0
     Dim Max_Volume_Ticker_Name As String
     Max_Volume_Ticker_Name = " "
     Dim Max_Volume As Double 
     Max_Volume = 0

'Now we set location for variables
Dim Summary_Table_Row As Long
Summary_Table_Row = 2

'And then we set row count for the workbook (all sheets)
Dim Lastrow As Long

'Loop through all sheets to find last cell that is not empty
Lastrow = MainWs.Cells(Rows.Count,1).End(xlUp).Row

'Set initial value of beginning stock value for the first Ticker of MainWs
Beg_Price = Mainws.Cells(2,3).Value

'Loop from beginning of the main worksheet (Row2) till its last Row of worksheet 7
For i = 2 to Lastrow

'Check if we are on the same ticker name
If MainWs.Cells(i+1,1).Value <> MainWs.Cells(i,1).Value Then

' Set the ticker name starting point
Ticker_Name = MainWs.Cells(i,1).Value

'Calculate
End_Price = MainWs.Cells(i,6).Value
Yearly_Price_Change = End_Price - Beg_Price

'Set condition for a zero value
If Beg_Price <> 0 Then
   Yearly_Price_Change_Percent = (Yearly_Price_Change/Beg_Price)*100

End If 

'Add to the Ticker name total volume
Total_Ticker_Volume = Total_Ticker_Volume + MainWs.Cells(i,7).Value

'Print the Ticker Name in the Summary Table, Column I
MainWs.Range("I" & Summary_Table_Row).Value = Ticker_Name

'Print the yearly price change in the summary table, Column J
MainWs.Range("J" & Summary_Table_Row).Value = Yearly_Price_Change

'Color fill yearly price change: Red for negative, green for a positive change
If (Yearly_Price_Change > 0) Then
    MainWs.Range("J" & Summary_Table_Row).Interior.ColorIndex = 4

ElseIf (Yearly_Price_Change <= 0) Then
    MainWs.Range("J" & Summary_Table_Row).Interior.ColorIndex = 3
End If 

'Print the yearly price change as a percent in the Summary Table, Column K
MainWs.Range("K" & Summary_Table_Row).Value = (CStr(Yearly_Price_Change_Percent) & "%")

'Print the total stock volume in the Summary table, Column L
MainWs.Range("L" & Summary_Table_Row).Value = Total_Ticker_Volume

'Add 1 to the Summary table row count
Summary_Table_Row = Summary_Table_Row + 1

'Now we get next beginning price
Beg_Price = MainWs.Cells(i+1,3).Value

'Do Calculations
If (Yearly_Price_Change_Percent > Max_Percent) Then
    Max_Percent = Yearly_Price_Change_Percent
    Max_Ticker_Name = Ticker_Name

ElseIf (Yearly_Price_Change_Percent < Min_Percent) Then
     Min_Percent = Yearly_Price_Change_Percent
     Min_Ticker_Name = Ticker_Name

End If

If (Total_Ticker_Volume > Max_Volume) Then
    Max_Volume = Total_Ticker_Volume
    Max_Volume_Ticker_Name = Ticker_Name
End If

'Reset values
Yearly_Price_Change_Percent = 0
Total_Ticker_Volume = 0

'Else if in next ticker name, enter new ticker stock volume
Else 

    Total_Ticker_Volume = Total_Ticker_Volume + MainWs.Cells(i,7).Value

End If

Next i 

        'Print values in assigned cells
        MainWs.Range("Q2").Value = (CStr(Max_Percent) & "%")
        MainWs.Range("Q3").Value = (CStr(Min_Percent) & "%")
        MainWs.Range("P2").Value = Max_Ticker_Name
        MainWs.Range("P3").Value = Min_Ticker_Name
        MainWs.Range("Q4").Value = Max_Volume
        MainWs.Range("O2").Value = "Greatest % Increase"
        MainWs.Range("O3").Value = "Greatest % Decrease"
        MainWs.Range("O4").Value = "Greatest Total Volume"

Next MainWs

End Sub
