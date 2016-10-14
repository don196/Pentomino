using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentomino
{
    class Figures
    {
        public int summa = 0;
        public int numberOfPlaces = 0;
        public int numberOfTetramino = 0;
        public int numberOfTrimino = 0;


        class Figure
        {
            sbyte[,] dots;
            bool symmetric;
            byte rotations;


            public Figure(sbyte[,] dots, bool symmetric, byte rotations)
            {
                this.dots = new sbyte[dots.GetLength(0), dots.GetLength(1)];
                for (int i = 0; i < dots.GetLength(0); i++)
                    for (int j = 0; j < dots.GetLength(1); j++)
                        this.dots[i, j] = dots[i, j];

                this.symmetric = symmetric;
                this.rotations = rotations;
            }


            public void changeDots(sbyte[,] dots)
            {
                this.dots = new sbyte[dots.GetLength(0), dots.GetLength(1)];
                for (int i = 0; i < dots.GetLength(0); i++)
                    for (int j = 0; j < dots.GetLength(1); j++)
                        this.dots[i, j] = dots[i, j];
            }


            public sbyte[,] getDots()
            {
                return dots;
            }


            public bool getSymmetric()
            {
                return symmetric;
            }


            public byte getRotations()
            {
                return rotations;
            }
        }


        static Figure[] addFigureSources = 
        {
            //I trimino
            new Figure(new sbyte[,] 
            {
                {0,0},
                {0,-1},
                {0,1},
            }, 
            true, 2),

            //L trimino
            new Figure(new sbyte[,] 
            {
                {0,0},
                {0,-1},
                {1,0},
            }, 
            true, 4),

            //I tetramino
            new Figure(new sbyte[,] 
            {
                {0,0},
                {0,-1},
                {0,1},
                {0,2},
            }, 
            true, 2),

            //L tetramino
            new Figure(new sbyte[,] 
            {
                {0,0},
                {0,-1},
                {0,1},
                {1,1},
            }, 
            false, 4),

            //S tetramino
            new Figure(new sbyte[,] 
            {
                {0,0},
                {0,-1},
                {1,0},
                {1,1},
            }, 
            false, 2),

            //T tetramino
            new Figure(new sbyte[,] 
            {
                {0,0},
                {-1,0},
                {1,0},
                {0,1},
            }, 
            true, 4),

            //O tetramino
            new Figure(new sbyte[,] 
            {
                {0,0},
                {1,0},
                {0,1},
                {1,1},
            }, 
            true, 1),
        };


        static Figure[] figureSources = 
        {
            //F
            new Figure(new sbyte[,] 
            {
                {0,0},
                {-1,0},
                {0,-1},
                {0,1},
                {1,-1},
            }, 
            false, 4),
            

            //L
            new Figure(new sbyte[,]
            {
                {0,0},
                {0,-1},
                {0,-2},
                {0,1},
                {1,1},
            },
            false, 4),

            
            //N
            new Figure(new sbyte[,]
            {
                {0,0},
                {0,-1},
                {0,-2},
                {-1,0},
                {-1,1},
            },
            false, 4),

            
            //P
            new Figure(new sbyte[,]
            {
                {0,0},
                {0,-1},
                {1,-1},
                {1,0},
                {0,1},
            },
            false, 4),
            

            //T
            new Figure(new sbyte[,]
            {
                {0,0},
                {0,-1},
                {-1,-1},
                {1,-1},
                {0,1},
            },
            true, 4),
            

            //U
            new Figure(new sbyte[,]
            {
                {0,0},
                {-1,0},
                {-1,-1},
                {1,0},
                {1,-1},
            },
            true, 4),
            

            //V
            new Figure(new sbyte[,]
            {
                {0,0},
                {-1,0},
                {-2,0},
                {0,-1},
                {0,-2},
            },
            true, 4),
            

            //W
            new Figure(new sbyte[,]
            {
                {0,0},
                {0,1},
                {-1,1},
                {1,0},
                {1,-1},
            },
            true, 4),
            

            //Y
            new Figure(new sbyte[,]
            {
                {0,0},
                {-1,0},
                {0,-1},
                {0,1},
                {0,2},
            },
            false, 4),


            //I
            new Figure(new sbyte[,]
            {
                {0,0},
                {0,-1},
                {0,-2},
                {0,1},
                {0,2},
            },
            true, 2),
            
            
            //X
            new Figure(new sbyte[,]
            {
                {0,0},
                {-1,0},
                {0,-1},
                {1,0},
                {0,1},
            },
            true, 1),
            
            
            //Z
            new Figure(new sbyte[,]
            {
                {0,0},
                {0,-1},
                {-1,-1},
                {0,1},
                {1,1},
            },
            false, 2)
        };


        sbyte[,] rotate(sbyte[,] figure)
        {
            sbyte[,] result = new sbyte[figure.GetLength(0), figure.GetLength(1)]; 
            for (int i = 0; i < figure.GetLength(0); i++)
            {
                sbyte x = figure[i,0];
                sbyte y = figure[i,1];
                y = (sbyte)(-1 * y);
                result[i, 0] = y;
                result[i, 1] = x;
            }
            return result;
        }


        sbyte[,] mirror(sbyte[,] figure)
        {
            sbyte[,] result = new sbyte[figure.GetLength(0), figure.GetLength(1)];
            for (int i = 0; i < figure.GetLength(0); i++)
            {
                sbyte x = figure[i, 0];
                sbyte y = figure[i, 1];
                y = (sbyte)(-1 * y);
                result[i, 0] = x;
                result[i, 1] = y;
            }
            return result;
        }


        public int getNumberOfBlankSpaces(bool isTrtimino = false, bool isTetramino = false, bool isPentamino = true)
        {
            int maxTrimino = 0, maxTetramino = 0, maxPentamino = 0;
            if (isTrtimino) maxTrimino = numberOfPlaces / 3;
            if (isTetramino) maxTetramino = numberOfPlaces / 4;
            if (isPentamino) maxPentamino = numberOfPlaces / 5;
            int minBlankSpaces = numberOfPlaces;
            for (int i = 0; i <= maxTrimino; i++ )
            {
                for(int j = 0; j <= maxTetramino; j++)
                {
                    for(int k = 0; k <= maxPentamino; k++)
                    {
                        if (i * 3 + j * 4 + k * 5 <= numberOfPlaces && numberOfPlaces - i * 3 - j * 4 - k * 5 < minBlankSpaces) minBlankSpaces = numberOfPlaces - i * 3 - j * 4 - k * 5;
                    }
                }
            }
            return minBlankSpaces;
        }


        public List<List<int>> formStructure(sbyte[][] area, bool isTrtimino = false, bool isTetramino = false, bool isPentamino = true)
        {
            int[] translateArray = new int[area.GetLength(0) * area[0].GetLength(0)];
            int sum = 0;
            for (int i = 0; i < area.GetLength(0); i++)
                for (int j = 0; j < area[0].GetLength(0); j++)
                    if (area[i][j] == 1)
                    {
                        translateArray[i * area[0].GetLength(0) + j] = sum;
                        sum++;
                    }
            summa = sum;
            numberOfPlaces = summa;
            List<List<int>> result = new List<List<int>>();
            int count = 0;
            if(isPentamino) count += figureSources.GetLength(0);
            if(isTrtimino) count += 2;
            if(isTetramino) count += 5;
            Figure[] figures = new Figure[count];
            count = 0;
            if(isPentamino)
            {
                for(int i = 0; i < figureSources.GetLength(0); i++)
                {
                    figures[count] = figureSources[i];
                    count++;
                }
            }
            if (isTrtimino)
            {
                for (int i = 0; i < 2; i++)
                {
                    figures[count] = addFigureSources[i];
                    count++;
                }
            }
            if (isTetramino)
            {
                for (int i = 2; i < 7; i++)
                {
                    figures[count] = addFigureSources[i];
                    count++;
                }
            }
            for (int i = 0; i < area.GetLength(0); i++)
            {
                for (int j = 0; j < area[0].GetLength(0); j++)
                {
                    if (area[i][j] == 1)
                    {
                        for (int figure = 0; figure < figures.GetLength(0); figure++)
                        {
                            Figure fig = figures[figure];
                            byte symmetric = 2;
                            if (fig.getSymmetric()) symmetric = 1;
                            for (byte sym = 0; sym < symmetric; sym++)
                            {
                                for (byte rotation = 0; rotation < fig.getRotations(); rotation++)
                                {
                                    List<int> tempList = new List<int>();
                                    bool flag = true;
                                    for (byte dot = 0; dot < fig.getDots().GetLength(0); dot++)
                                    {
                                        int row = i + fig.getDots()[dot, 0];
                                        int col = j + fig.getDots()[dot, 1];
                                        try
                                        {
                                            if (area[row][col] != 1)
                                            {
                                                flag = false;
                                                break;
                                            }
                                        }
                                        catch (IndexOutOfRangeException)
                                        {
                                            flag = false;
                                            break;
                                        }
                                        int index = row * area[0].GetLength(0) + col;
                                        int place = translateArray[index];
                                        tempList.Add(place);
                                    }
                                    if (flag)
                                    {
                                        result.Add(tempList);
                                    }
                                    if (!(rotation + 1 == fig.getRotations())) fig.changeDots(rotate(fig.getDots()));
                                }
                                if (!fig.getSymmetric()) fig.changeDots(mirror(fig.getDots()));
                            }
                        }
                    }
                }
            }
            return result;
        }


        public List<List<int>> addDifferPolimino(List<List<int>> inputList, sbyte[][] area, List<int> indexesOfPolimino)
        {
            int[] translateArray = new int[area.GetLength(0) * area[0].GetLength(0)];
            int sum = 0;
            for (int i = 0; i < area.GetLength(0); i++)
                for (int j = 0; j < area[0].GetLength(0); j++)
                    if (area[i][j] == 1)
                    {
                        translateArray[i * area[j].GetLength(0) + j] = sum;
                        sum++;
                    }
            bool[] polAdded = new bool[indexesOfPolimino.Count];
            for (int i = 0; i < area.GetLength(0); i++)
            {
                for (int j = 0; j < area[0].GetLength(0); j++)
                {
                    if (area[i][j] == 1)
                    {
                        for (int figure = 0; figure < indexesOfPolimino.Count; figure++)
                        {
                            bool figureAdded = false;
                            Figure fig = addFigureSources[indexesOfPolimino[figure]];
                            byte symmetric = 2;
                            if (fig.getSymmetric()) symmetric = 1;
                            for (byte sym = 0; sym < symmetric; sym++)
                            {
                                for (byte rotation = 0; rotation < fig.getRotations(); rotation++)
                                {
                                    List<int> tempList = new List<int>();
                                    bool flag = true;
                                    for (byte dot = 0; dot < fig.getDots().GetLength(0); dot++)
                                    {
                                        int row = i + fig.getDots()[dot, 0];
                                        int col = j + fig.getDots()[dot, 1];
                                        try
                                        {
                                            if (area[row][col] != 1)
                                            {
                                                flag = false;
                                                break;
                                            }
                                        }
                                        catch (IndexOutOfRangeException)
                                        {
                                            flag = false;
                                            break;
                                        }
                                        int index = row * area[0].GetLength(0) + col;
                                        int place = translateArray[index];
                                        tempList.Add(place);
                                    }
                                    if (flag)
                                    {
                                        tempList.Add(summa + figure);
                                        figureAdded = true;
                                        inputList.Add(tempList);
                                    }
                                    if (!(rotation + 1 == fig.getRotations())) fig.changeDots(rotate(fig.getDots()));
                                }
                                if (!fig.getSymmetric()) fig.changeDots(mirror(fig.getDots()));
                            }
                            if (figureAdded)
                            {
                                polAdded[figure] = true;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < indexesOfPolimino.Count; i++ )
            {
                if(polAdded[i])
                {
                    if (indexesOfPolimino[i] < 2)
                        numberOfTrimino++;
                    else
                        numberOfTetramino++;
                    summa++;
                }
            }
            return inputList;
        }


        public List<List<int>> addBlankSpaces(List<List<int>> inputList, bool isTrtimino = false, bool isTetramino = false, bool isPentamino = true)
        {
            int numberOfBlankSpaces = getNumberOfBlankSpaces(isTrtimino, isTetramino, isPentamino);
            for (int i = 0; i < numberOfBlankSpaces; i++)
            {
                for (int j = 0; j < numberOfPlaces; j++)
                {
                    List<int> tempList = new List<int>();
                    tempList.Add(j);
                    tempList.Add(summa + i);
                    inputList.Add(tempList);
                }
            }
            summa += numberOfBlankSpaces;
            return inputList;
        }
    }
}
