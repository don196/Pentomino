using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Pentomino
{
    public partial class MainForm : Form
    {
        public class Worker
        {
            bool isMod;
            MainForm form;
            SynchronizationContext uiContext;
            public CancellationToken token;


            public void doWork(List<List<int>> input, int summa, MainForm form, SynchronizationContext uiContext, CancellationToken token, bool isMod = false)
            {
                this.uiContext = uiContext;
                this.form = form;
                this.isMod = isMod;
                this.token = token;
                token.ThrowIfCancellationRequested();
                Algorithm alg = new Algorithm(this);
                alg.Restruct(input, summa);
                if (isMod)
                    alg.StartMultiSteps();
                else
                    alg.Step();
            }


            public void SolutionFound(Algorithm alg)
            {
                if (isUnique(alg.solution))
                {
                    List<int> sol = new List<int>();
                    for (int i = 0; i < alg.solution.Count; i++)
                    {
                        sol.Add(alg.solution[i]);
                    }
                    uiContext.Send(UpdateUI, sol);
                }
                token.ThrowIfCancellationRequested();
            }


            public void SolutionFound()
            {
                token.ThrowIfCancellationRequested();
            }


            private void UpdateUI(object state)
            {
                List<int> sol = state as List<int>;
                form.SolutionFound(sol);
            }


            private bool isUnique(List<int> sol)
            {
                bool result = true;
                foreach(List<int> s in form.solution)
                {
                    if(sol.Count == s.Count)
                    {
                        bool isEqual = true;
                        foreach(int row1 in s)
                        {
                            bool isThereRow1 = false;
                            foreach(int row2 in sol)
                            {
                                bool isRow1EqualRow2 = true;
                                if(form.input[row1].Count == (form.input[row2]).Count)
                                {
                                    if (form.input[row1].Count == 2)
                                    {
                                            if (form.input[row2][0] != form.input[row1][0])
                                            {
                                                isRow1EqualRow2 = false;
                                            }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < form.input[row2].Count; i++)
                                        {
                                            if (form.input[row2][i] != form.input[row1][i])
                                            {
                                                isRow1EqualRow2 = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    isRow1EqualRow2 = false;
                                }
                                if(isRow1EqualRow2)
                                {
                                    isThereRow1 = true;
                                    break;
                                }
                            }
                            if (!isThereRow1) 
                            {
                                isEqual = false;
                                break;
                            }
                        }
                        if (isEqual)
                        {
                            result = false;
                            break;
                        }
                    }
                }
                return result;
            }
        }
        Graphics g;
        sbyte[][] area;
        static Brush[] brushes = { Brushes.GreenYellow, Brushes.Red, Brushes.Blue, Brushes.Brown, Brushes.Yellow, Brushes.Pink, Brushes.Indigo, Brushes.Orange, Brushes.Coral };
        int W = 70;
        int H = 70;
        public delegate void listDel(string ps);
        int curSol = -1;
        List<List<int>> solution = new List<List<int>>();
        List<List<int>> input;
        bool solutionFound = false;
        Worker worker;
        CancellationTokenSource tokenSource;
        public MainForm()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            area = new sbyte[][] { new sbyte[] { 1, 1, 0, }, new sbyte[] { 1, 1, 1, }, new sbyte[] { 1, 1, 0, } };
            usePentaminoCheck.Checked = true;
            label1.Text = "";
            NextSolButton.Enabled = false;
            PrevSolButton.Enabled = false;
            stopButton.Enabled = false;
        }

        public async void runAlgo()
        {
            tokenSource = new CancellationTokenSource();
            TaskCompletionSource<object> completeSource = new TaskCompletionSource<object>();
            tokenSource.Token.Register(() => completeSource.TrySetCanceled());
            clearSolutionArea();
            this.panel1.Invalidate();
            solution = new List<List<int>>();
            solutionFound = false;
            Figures figures = new Figures();
            worker = new Worker();
            clearSolutionArea();
            int[] scopes = getScopes();
            sbyte[][] usefulArea = getUsefulArea(scopes);
            SynchronizationContext uiContext = SynchronizationContext.Current;
            curSol = -1;
            label1.Text = "";
            label1.Invalidate();
            lockControls();
            bool messageBoxYes = false;
            int range = usefulArea.GetLength(0);
            if (usefulArea.GetLength(0) != 0)
            {
                input = await Task.Factory.StartNew<List<List<int>>>(
                                                 () => figures.formStructure(usefulArea, useTriminoCheck.Checked, useTetraminoCheck.Checked, usePentaminoCheck.Checked),
                                                 TaskCreationOptions.LongRunning);
                if (isModAlgBox.Checked)
                {
                    stopButton.Enabled = true;
                    try
                    {
                        await Task.Factory.StartNew(
                                                     () => worker.doWork(input, figures.summa, this, uiContext, tokenSource.Token, true),
                                                     TaskCreationOptions.LongRunning).ContinueWith(tsk =>
                                                     { }, TaskContinuationOptions.OnlyOnFaulted);
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    bool isSolvable = true;
                    if (blankCellsCheck.Checked)
                        input = await Task.Factory.StartNew<List<List<int>>>(
                                                 () => figures.addBlankSpaces(input, useTriminoCheck.Checked, useTetraminoCheck.Checked, usePentaminoCheck.Checked),
                                                 TaskCreationOptions.LongRunning);
                    else
                        if (figures.getNumberOfBlankSpaces(useTriminoCheck.Checked, useTetraminoCheck.Checked, usePentaminoCheck.Checked) != 0) isSolvable = false;
                    if (isSolvable)
                    {
                        Algorithm alg = new Algorithm(worker);
                        stopButton.Enabled = true;
                        try
                        {
                            await Task.Factory.StartNew(
                                                 () => worker.doWork(input, figures.summa, this, uiContext, tokenSource.Token),
                                                 TaskCreationOptions.LongRunning).ContinueWith(tsk =>
                                                 { }, TaskContinuationOptions.OnlyOnFaulted);
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Решение нельзя найти для такой формы. Добавить пустые клетки программно?", "Добавить пустые клетки?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes) messageBoxYes = true;
                    }
                }
            }
            else
            {
                messageBoxYes = false;
            }
            if(!messageBoxYes)
            {
                unlockControls();
                if (!solutionFound)
                    label1.Text = "Решений нет";
                this.panel1.Invalidate();
            }
            else
            {
                blankCellsCheck.Checked = true;
                runAlgo();
            }
        }


        public void SolutionFound(List<int> solution)
        {
            if(solution.Count != 0)
            {
                this.solution.Add(solution);
                label1.Text = "Количество найденных решений: " + this.solution.Count.ToString();
                label1.Invalidate();
                if (!solutionFound)
                {
                    clearSolutionArea();
                    paintSolution(solution);
                    solutionFound = true;
                    this.panel1.Invalidate();
                    curSol = 0;
                }
                else
                {
                    if(NextSolButton.InvokeRequired)
                    {

                        if (!NextSolButton.Enabled) NextSolButton.Enabled = true;
                    }
                    else
                    {
                        if (!NextSolButton.Enabled) NextSolButton.Enabled = true;
                    }
                }
            }
        }


        private void paintSolution(List<int> solution)
        {
            
            int sum = 0;
            for (int i = 0; i < area.GetLength(0); i++)
            {
                for (int j = 0; j < area[0].GetLength(0); j++)
                    if (area[i][j] == 1)
                    {
                        sum++;
                    }
            }
            int[,] translateArray = new int[sum, 2];
            sum = 0;
            for (int i = 0; i < area.GetLength(0); i++)
            {
                for (int j = 0; j < area[0].GetLength(0); j++)
                    if (area[i][j] == 1)
                    {
                        translateArray[sum,0] = i;
                        translateArray[sum,1] = j;
                        sum++;
                    }
            }
            for(int i = 0; i < solution.Count; i++)
            {
                if (input[solution[i]].Count != 2)
                for (int j = 0; j < input[solution[i]].Count; j++)
                {
                    if (input[solution[i]][j] < sum)
                    {
                        area[translateArray[input[solution[i]][j], 0]][translateArray[input[solution[i]][j], 1]] = (sbyte)(2 + i % brushes.GetLength(0));
                    }
                }
                else
                    area[translateArray[input[solution[i]][0], 0]][translateArray[input[solution[i]][0], 1]] = (sbyte)(2 + brushes.GetLength(0));
            }
        }

        private void paintArea(Graphics g)
        {
            for (int i = 0; i < area.GetLength(0); i++)
                for (int j = 0; j < area[0].GetLength(0); j++)
                    paintCell(g, i, j);
        }

        private void paintCell(Graphics g, int row, int col)
        {
            int x, y;

            x = (col) * W + 1;
            y = (row) * H + 1;
            if(area[row][col] == 1)
                g.FillRectangle(Brushes.Aqua, x - 1, y - 1, W, H);
            else if (area[row][col] == 0)
                g.FillRectangle(Brushes.DarkGray,
                        x - 1, y - 1, W, H);
            else if (area[row][col] == brushes.GetLength(0) + 2)
                g.FillRectangle(Brushes.Gray,
                        x - 1, y - 1, W, H);
            else
                g.FillRectangle(brushes[area[row][col] - 2],
                        x - 1, y - 1, W, H);
            g.DrawRectangle(Pens.Black,
                x - 1, y - 1, W, H);
        }


        private int[] getScopes()
        {
            int[] scopes = new int[4];
            scopes[0] = area[0].GetLength(0);
            scopes[1] = area.GetLength(0);
            scopes[2] = 0;
            scopes[3] = 0;
            for (int i = 0; i < area.GetLength(0); i++)
                for (int j = 0; j < area[i].GetLength(0); j++ )
                {
                    if(area[i][j] == 1)
                    {
                        if (j < scopes[0]) scopes[0] = j;
                        if (i < scopes[1]) scopes[1] = i;
                        if (j > scopes[2]) scopes[2] = j;
                        if (i > scopes[3]) scopes[3] = i;
                    }
                }
            return scopes;
        }


        private sbyte[][] getUsefulArea(int[] scopes)
        {
            sbyte[][] usefulArea;
            if (scopes[3] - scopes[1] >= 0)
            {
                usefulArea = new sbyte[scopes[3] - scopes[1] + 1][];
                for (int i = 0; i < usefulArea.GetLength(0); i++)
                {
                    usefulArea[i] = new sbyte[scopes[2] - scopes[0] + 1];
                    for (int j = 0; j < usefulArea[i].GetLength(0); j++)
                    {
                        usefulArea[i][j] = area[scopes[1] + i][scopes[0] + j];
                    }
                }
            }
            else
            {
                usefulArea = new sbyte[0][];
            }
            return usefulArea;
        }


        private void clearSolutionArea()
        {
            for (int i = 0; i < area.GetLength(0); i++)
                for (int j = 0; j < area[i].GetLength(0); j++)
                    if (area[i][j] > 1) area[i][j] = 1;
        }


        public void lockControls()
        {
            startAlgButton.Enabled = false;
            buildAreaButton.Enabled = false;
            blankCellsCheck.Enabled = false;
            usePentaminoCheck.Enabled = false;
            useTetraminoCheck.Enabled = false;
            useTriminoCheck.Enabled = false;
            isModAlgBox.Enabled = false;
            PrevSolButton.Enabled = false;
            NextSolButton.Enabled = false;
            panel1.Enabled = false;
            saveFormButton.Enabled = false;
            loadFormButton.Enabled = false;
        }


        public void unlockControls()
        {
            startAlgButton.Enabled = true;
            buildAreaButton.Enabled = true;
            usePentaminoCheck.Enabled = true;
            useTetraminoCheck.Enabled = true;
            useTriminoCheck.Enabled = true;
            isModAlgBox.Enabled = true;
            if (!isModAlgBox.Checked) blankCellsCheck.Enabled = true;
            panel1.Enabled = true;
            stopButton.Enabled = false;
            saveFormButton.Enabled = true;
            loadFormButton.Enabled = true;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            paintArea(g);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            int row = (int)(e.Y / H);
            int col = (int)(e.X / W);
            try
            {
                if (area[row][col] == 1)
                    area[row][col] = 0;
                else
                    area[row][col] = 1;
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
            this.panel1.Invalidate();
        }


        private void startAlgButton_Click(object sender, EventArgs e)
        {
            runAlgo();
        }


        private void buildAreaButton_Click(object sender, EventArgs e)
        {
            try
            {
                int row = int.Parse(rowBox.Text);
                int col = int.Parse(colBox.Text);
                if (row < 1 || col < 1) throw new FormatException();
                if (row * col > 2500) throw new FormatException();
                int tempH = panel1.Height / row;
                int tempW = panel1.Width / col;
                if (tempH < 5) H = 5;
                else if (tempH > 70) H = 70;
                    else H = tempH;
                if (tempW < 5) W = 5;
                else if (tempW > 70) W = 70;
                    else W = tempW;
                area = new sbyte[row][];
                for (int i = 0; i < row; i++)
                {
                    area[i] = new sbyte[col];
                    for (int j = 0; j < col; j++)
                    {
                        area[i][j] = 1;
                    }
                }
                this.panel1.Invalidate();
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректные размеры поля");
            }
        }


        private void isModAlgBox_CheckedChanged(object sender, EventArgs e)
        {
            if(isModAlgBox.Checked)
            {
                blankCellsCheck.Enabled = false;
            }
            else
            {
                blankCellsCheck.Enabled = true;
            }
        }


        private void PrevSolButton_Click(object sender, EventArgs e)
        {
            curSol--;
            clearSolutionArea();
            paintSolution(solution[curSol]);
            this.panel1.Invalidate();
            NextSolButton.Enabled = true;
            if(curSol == 0)
            {
                PrevSolButton.Enabled = false;
            }
        }


        private void NextSolButton_Click(object sender, EventArgs e)
        {
            curSol++;
            clearSolutionArea();
            paintSolution(solution[curSol]);
            this.panel1.Invalidate();
            PrevSolButton.Enabled = true;
            if (curSol == solution.Count - 1)
            {
                NextSolButton.Enabled = false;
            }
        }


        private void saveFormButton_Click(object sender, EventArgs e)
        {
            clearSolutionArea();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter str = new StreamWriter(saveFileDialog.FileName);
                    sbyte[][] area = getUsefulArea(getScopes());
                    for (int i = 0; i < area.GetLength(0); i++ )
                    {
                        string line = "";
                        for (int j = 0; j < area[i].GetLength(0); j++)
                        {
                            line += area[i][j].ToString();
                        }
                        str.WriteLine(line);
                    }
                    str.Close();
            }
        }


        private void loadFormButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader str = new StreamReader(openFileDialog.FileName);
                string s;
                int size = 0;
                int count = -1;
                bool isNormal = true;
                while((s = str.ReadLine()) != null)
                {
                    if(count != -1 && count != s.Length) isNormal = false;
                    count = s.Length;
                    size++;
                }
                str.Close();
                if(isNormal)
                {
                    str = new StreamReader(openFileDialog.FileName);
                    sbyte[][] area = new sbyte[size][];
                    int current = 0;
                    while ((s = str.ReadLine()) != null)
                    {
                        area[current] = new sbyte[s.Length];
                        for(int i = 0; i < s.Length; i++)
                        {
                            try
                            {
                                area[current][i] = sbyte.Parse(s.ToCharArray()[i].ToString());
                                if (area[current][i] != 0 && area[current][i] != 1) isNormal = false;
                            }
                            catch (FormatException)
                            {
                                isNormal = false;
                                break;
                            }
                        }
                        if (!isNormal) break;
                        current++;
                    }
                    if (isNormal && current != 0)
                    {
                        this.area = area;
                        int tempH = panel1.Height / area.GetLength(0);
                        int tempW = panel1.Width / area[0].GetLength(0);
                        if (tempH < 5) H = 5;
                        else if (tempH > 70) H = 70;
                        else H = tempH;
                        if (tempW < 5) W = 5;
                        else if (tempW > 70) W = 70;
                        else W = tempW;
                    }
                    else MessageBox.Show("Некорректный формат файла для загрузки формы");
                    this.panel1.Invalidate();
                }
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if(tokenSource != null)
            {
                tokenSource.Cancel();
            }
        }

        private void aboutProgram_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа составления фигур из пентамино\n\nВ программе присутствует возможность:" + 
                "\nРешение задачи пентамино для произвольной формы с возможностью использованием объектов пентамино повторно\n" +
                "Решение задачи пентамино для форм, которые заведомо не могут быть заполнены, путем программного добавления пустых клеток\n" + 
                "Использование различных объектов полимино (пентамино, тетрамино, тримино) для решения задачи\n" + 
                "Решение задачи раскроя на примере задачи пентамино с помощью модифицированного алгоритма \"Dancing Links\"" + 
                "\n\nПрограмму выполнил: Чижов Владислав Андреевич" +
                "\nniternod@gmail.com");
        }
    }
}
