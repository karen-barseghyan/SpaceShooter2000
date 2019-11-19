private async Task LoadSettings(Player player)
        {
            DebugLine.Text += Directory.GetCurrentDirectory() + "\n";
            try
            {
                using (StreamReader sr = new StreamReader("../../config.txt"))
                {
                    KeyConverter kc = new KeyConverter();
                    MouseActionConverter mc = new MouseActionConverter();
                    string line = await sr.ReadToEndAsync();
                    //DebugLine.Text += line + "\n";
                    string[] lines = line.Split('\n');
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] l = lines[i].Split(':');
                        if (l.Length > 1)
                        {
                            DebugLine.Text += "name: " + l[0];
                            string[] inputs = l[1].Split(' ');
                            DebugLine.Text += ", inputs: ";
                            
                            for (int j = 0; j < inputs.Length-1; j++)
                            {
                                if (inputs[j].Length > 0)
                                {
                                    try
                                    {
                                        Key test = (Key)kc.ConvertFromString(inputs[j]);
                                        DebugLine.Text += test + ", ";
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            MouseAction test = (MouseAction)mc.ConvertFromString(inputs[j]);
                                            DebugLine.Text += test + ", ";
                                        }
                                        catch (Exception e)
                                        {
                                            DebugLine.Text += e.ToString();
                                        }
                                    }
                                }
                            }
                            DebugLine.Text += "\n";
                        }
                    }
                    //player.inputs.Add(new Input())
                }
            }
            catch (FileNotFoundException ex)
            {
                DebugLine.Text += ex.Message;
            }
        }


private void InitializeKeyInputs()
        {
            goLeft1 = Key.A;
            goLeft2 = Key.Left;
            inputs.Add(new Input("Go Left", goLeft1, goLeft2));

            goRight1 = Key.A;
            goRight2 = Key.Right;
            inputs.Add(new Input("Go Right", goRight1, goRight2));

            goUp1 = Key.W;
            goUp2 = Key.Up;
            inputs.Add(new Input("Go Up", goUp1, goUp2));

            goDown1 = Key.S;
            goDown2 = Key.Down;
            inputs.Add(new Input("Go Down", goDown1, goDown2));

            shoot1 = Key.Space;
            shoot2 = Key.E;
            shoot1mouse = MouseAction.LeftClick;
            inputs.Add(new Input("Shoot", shoot1, shoot2, shoot1mouse));

            shoot2mouse = MouseAction.RightClick;
            inputs.Add(new Input("Alt Shoot", shoot2mouse));
            

            bomb = Key.B;
            inputs.Add(new Input("Bomb", bomb));

            pause = Key.Escape;
            inputs.Add(new Input("Pause", pause));

            slow1 = Key.LeftShift;
            inputs.Add(new Input("Slow", slow1));

            disableMouse = false;

            /*
            mainWindow.DebugWrite(inputs[4].ToString());
            string[] lines = new string [inputs.Count];
            int i = 0;
            foreach (Input input in inputs)
                lines[i++] = input.ToString();
            System.IO.File.WriteAllLines("../../config.txt", lines, Encoding.UTF8);*/
        }

/*
Go Left: A Left 
Go Right: A Right 
Go Up: W Up 
Go Down: S Down 
Shoot: Space E LeftClick 
Alt Shoot: RightClick 
Bomb: B 
Pause: Escape 
Slow: LeftShift
*/