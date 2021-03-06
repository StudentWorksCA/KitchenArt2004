Imports System.Math
<Serializable()> _
Public Class Door
    Inherits WallKitchenItem

    Public Sub New()
        MyBase.New()
        metricLength = 100
        Me.IsSelected = True
    End Sub
    Public Sub New(ByVal gridCellSize As Integer, ByVal metricCellSize As Integer)
        Me.New()
        Me.gridCellSize = gridCellSize
        Me.metricCellSize = metricCellSize
    End Sub

    Public Overrides Function isOutOfBoundariesOf(ByVal rect As Rectangle, ByVal boundaryWidth As Integer) As Boolean
        Dim unitX As Integer = Abs((pixX + pixLength / 2) - (rect.X + rect.Width / 2))
        Dim unitY As Integer = Abs((pixY + pixWidth / 2) - (rect.Y + rect.Height / 2))

        If (((Abs(unitX) > (rect.Width / 2) And Abs(unitX) < rect.Width / 2 + boundaryWidth) _
            And Abs(unitY) < rect.Height / 2 - pixWidth / 2 + boundaryWidth) And pixLength / 2 + rect.Width / 2 <= unitX) _
                Or _
           (((Abs(unitY) > (rect.Height / 2) And Abs(unitY) < rect.Height / 2 + boundaryWidth) _
            And Abs(unitX) < rect.Width / 2 - pixLength / 2 + boundaryWidth) And pixWidth / 2 + rect.Height / 2 <= unitY) _
        Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Overrides Sub Draw(ByVal gr As System.Drawing.Graphics)

        gr.FillRectangle(Brushes.White, Me.pixX, Me.pixY, Me.pixLength, Me.pixWidth)
        Dim doorLen As Integer
        Select Case Me.curRotation
            Case 0
                doorLen = Me.pixLength / 3
                gr.DrawLine(Pen, Me.pixX, Me.pixY, Me.pixX, Me.pixY + Me.pixWidth)
                gr.DrawLine(Pen, Me.pixX + Me.pixLength, Me.pixY, Me.pixX + Me.pixLength, Me.pixY + Me.pixWidth)
                gr.DrawLine(Pen, Me.pixX, Me.pixY + Me.pixWidth \ 2, Me.pixX + doorLen, Me.pixY + Me.pixWidth \ 2)
                gr.DrawLine(Pen, Me.pixX + Me.pixLength - doorLen, Me.pixY + Me.pixWidth \ 2, _
                                Me.pixX + Me.pixLength, Me.pixY + Me.pixWidth \ 2)

            Case 1
                doorLen = Me.pixWidth / 3
                gr.DrawLine(Pen, Me.pixX, Me.pixY, Me.pixX + Me.pixLength, Me.pixY)
                gr.DrawLine(Pen, Me.pixX, Me.pixY + Me.pixWidth, Me.pixX + Me.pixLength, Me.pixY + Me.pixWidth)
                gr.DrawLine(Pen, Me.pixX + Me.pixLength \ 2, Me.pixY, Me.pixX + Me.pixLength \ 2, _
                                    Me.pixY + doorLen)
                gr.DrawLine(Pen, Me.pixX + Me.pixLength \ 2, Me.pixY + Me.pixWidth - doorLen, _
                                Me.pixX + Me.pixLength \ 2, Me.pixY + Me.pixWidth)

        End Select
        If IsSelected = True Then
            DrawMeasurments(gr)
        End If

    End Sub

    Public Overrides Sub Rotate()
        If Me.curRotation < 1 Then
            Me.curRotation += 1
        Else
            Me.curRotation = 0
        End If

        MyBase.Rotate()
    End Sub

End Class
