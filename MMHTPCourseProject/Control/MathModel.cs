using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MMHTPCourseProject.Chart;
using MMHTPCourseProject.Model;

namespace MMHTPCourseProject.Control
{
    public class MathModel : IMathModel
    {
        public async Task<double> Process(InputData inputData)
        {
            return await Task.Run(() =>
            {
                var result = 0.0;
                try
                {
                    var W = 4 * inputData.Nu / (3600 * Math.PI * Math.Pow(inputData.D, 2));
                    var epsilon = inputData.Delta / inputData.D;
                    var Re = W*inputData.D*inputData.Ro/inputData.Mu;
                    double lambda = 0;
                    if (Re < 2320)
                    {
                        lambda = 64/Re;

                    }
                    else
                    {
                        if (Re<10/epsilon)
                        {
                            lambda = 0.316/Math.Pow(Re, 0.25);
                        }
                        else
                        {
                            if (Re < 560/epsilon)
                            {
                                lambda = 0.11*Math.Pow(epsilon + 68/Re, 0.25);
                            }
                            else
                            {
                                lambda = 0.11*Math.Pow(epsilon, 0.25);
                            }
                        }
                    }
                    var deltaP = lambda*inputData.L*Math.Pow(W, 2)*inputData.Ro/(2*inputData.D);
                    deltaP += 2*KsiB(inputData.D)*(Math.Pow(W, 2)/2)*inputData.Ro;
                    deltaP += inputData.Items.Select(t => A(t.Phi)*B(t.R/inputData.D)).Select(PsiP => PsiP*(Math.Pow(W, 2)/2)*inputData.Ro).Sum();
                    for (var i = inputData.Items.Count; i < inputData.N; i++)
                    {
                        var PsiP = A(0) * B(0);
                        deltaP += PsiP * (Math.Pow(W, 2) / 2) * inputData.Ro;
                    }
                    result = Math.Round(deltaP,2);
                }
                catch (DivideByZeroException)
                {
                    
                }

                return result;
            });
        }
        public async Task<ObservableCollection<DataSeriesInfo>> ProcessPressure(InputData inputData)
        {
            return await Task.Run(() =>
            {
                var result = new ObservableCollection<DataSeriesInfo>();
                var collection = new List<DataSeriesItem>();
                try
                {
                    for (var W = inputData.WStart; W < inputData.WEnd; W += inputData.H)
                    {
                        var epsilon = inputData.Delta/inputData.D;
                        var Re = W*inputData.D*inputData.Ro/inputData.Mu;
                        double lambda = 0;
                        if (Re < 2320)
                        {
                            lambda = 64/Re;

                        }
                        else
                        {
                            if (Re < 10/epsilon)
                            {
                                lambda = 0.316/Math.Pow(Re, 0.25);
                            }
                            else
                            {
                                if (Re < 560/epsilon)
                                {
                                    lambda = 0.11*Math.Pow(epsilon + 68/Re, 0.25);
                                }
                                else
                                {
                                    lambda = 0.11*Math.Pow(epsilon, 0.25);
                                }
                            }
                        }
                        var deltaP = lambda*inputData.L*Math.Pow(W, 2)*inputData.Ro/(2*inputData.D);
                        deltaP += 2*KsiB(inputData.D)*(Math.Pow(W, 2)/2)*inputData.Ro;
                        deltaP +=
                            inputData.Items.Select(t => A(t.Phi)*B(t.R/inputData.D))
                                .Select(PsiP => PsiP*(Math.Pow(W, 2)/2)*inputData.Ro)
                                .Sum();
                        for (var i = inputData.Items.Count; i < inputData.N; i++)
                        {
                            var PsiP = A(0)*B(0);
                            deltaP += PsiP*(Math.Pow(W, 2)/2)*inputData.Ro;
                        }
                        collection.Add(new DataSeriesItem { X = Math.Round(W, 2), Y = Math.Round(deltaP, 2) });
                    }
                }
                catch (DivideByZeroException)
                {

                }
                result.Add(new DataSeriesInfo {DataSeriesItems = collection, DataSeriesName = "Зависимость ΔP от W"});
                return result;
            });
        }
        public async Task<ObservableCollection<DataSeriesInfo>> ProcessDensity(InputData inputData)
        {
            return await Task.Run(() =>
            {
                var result = new ObservableCollection<DataSeriesInfo>();
                var collection = new List<DataSeriesItem>();
                try
                {
                    foreach (var substance in inputData.Substances)
                    {
                        var Ro = substance.Density;
                        var W = 4*inputData.Nu/(3600*Math.PI*Math.Pow(inputData.D, 2));
                        var epsilon = inputData.Delta/inputData.D;
                        var Re = W * inputData.D * Ro / inputData.Mu;
                        double lambda = 0;
                        if (Re < 2320)
                        {
                            lambda = 64/Re;

                        }
                        else
                        {
                            if (Re < 10/epsilon)
                            {
                                lambda = 0.316/Math.Pow(Re, 0.25);
                            }
                            else
                            {
                                if (Re < 560/epsilon)
                                {
                                    lambda = 0.11*Math.Pow(epsilon + 68/Re, 0.25);
                                }
                                else
                                {
                                    lambda = 0.11*Math.Pow(epsilon, 0.25);
                                }
                            }
                        }
                        var deltaP = lambda * inputData.L * Math.Pow(W, 2) * Ro / (2 * inputData.D);
                        deltaP += 2 * KsiB(inputData.D) * (Math.Pow(W, 2) / 2) * Ro;
                        deltaP +=
                            inputData.Items.Select(t => A(t.Phi)*B(t.R/inputData.D))
                                .Select(PsiP => PsiP * (Math.Pow(W, 2) / 2) * Ro)
                                .Sum();
                        for (var i = inputData.Items.Count; i < inputData.N; i++)
                        {
                            var PsiP = A(0)*B(0);
                            deltaP += PsiP * (Math.Pow(W, 2) / 2) * Ro;
                        }
                        collection.Add(new DataSeriesItem { X = Math.Round(Ro, 2), Y = Math.Round(deltaP, 2) });
                    }
                }
                catch (DivideByZeroException)
                {

                }
                result.Add(new DataSeriesInfo { DataSeriesItems = collection, DataSeriesName = "Зависимость ΔP от ρ" });
                return result;
            });
        }

        private double KsiB(double DT)
        {
            const double a = 1.21817696304E+000;
            const double b = 1.02482332600E+000;
            const double c = 6.01187009687E+001;
            const double d = -1.10310592872E+000;
            return a - b * Math.Exp(-c * Math.Pow(DT,d));
        }
        private double A(double PhiT)
        {
            const double a = -9.82062072341E-003;
            const double b = 1.75082299100E-002;
            const double c = -8.55036659762E-005;
            const double d = 1.75837055403E-007;
            return a + b * PhiT + c * Math.Pow(PhiT,2) + d * Math.Pow(PhiT,3);
        }
        private double B(double RD)
        {
            const double a = 5.20672618851E-001;
            const double b = 1.76921922457E-001;
            const double c = 2.24061666683E+000;
            const double d = 8.26225061752E-002;
            return (a + b * RD) / (1 + c * RD + d * Math.Pow(RD,2));
        }

    }
}
