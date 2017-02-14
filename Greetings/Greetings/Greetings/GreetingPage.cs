﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Greetings
{
    public class GreetingPage : ContentPage
    {
        public GreetingPage()
        {
            StackLayout stackLayout = new StackLayout();

            foreach(FieldInfo info in typeof(Color).GetRuntimeFields())
            {
                if (info.GetCustomAttribute<ObsoleteAttribute>() != null)
                    continue;

                if(info.IsPublic && 
                    info.IsStatic &&
                    info.FieldType == typeof(Color))
                {
                    stackLayout.Children.Add(CreateColorView((Color)info.GetValue(null), info.Name));
                }
            }

            foreach(PropertyInfo info in typeof(Color).GetRuntimeProperties())
            {
                MethodInfo methodInfo = info.GetMethod;

                if(methodInfo.IsPublic &&
                    methodInfo.IsStatic &&
                    methodInfo.ReturnType == typeof(Color))
                {
                    stackLayout.Children.Add(
                        CreateColorView((Color)info.GetValue(null), info.Name));
                }
            }

            Padding = new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5);

            Content = new ScrollView
            {
                Content = stackLayout
            };
        }

        private View CreateColorView(Color color, string name)
        {
            return new Frame
            {
                OutlineColor = Color.Accent,
                Padding = new Thickness(5),
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 15,
                    Children =
                    {
                        new BoxView
                        {
                            Color = color
                        },
                        new Label
                        {
                            Text = name,
                            FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Label)),
                            FontAttributes = FontAttributes.Bold,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        },
                        new StackLayout
                        {
                            Children =
                            {
                                new Label
                                {
                                    Text = String.Format("{0:X2}-{1:X2}-{2:X2}",
                                                          (int)(255 * color.R),
                                                          (int)(255 * color.G),
                                                          (int)(255 * color.B)),
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                    IsVisible = color != Color.Default
                                },
                                new Label
                                {
                                    Text = String.Format("{0:F2}-{1:F2}-{2:F2}",
                                                          color.Hue,
                                                          color.Saturation,
                                                          color.Luminosity),
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                    IsVisible = color != Color.Default
                                }
                            },
                            HorizontalOptions = LayoutOptions.End
                        }
                    }
                }
            };
        }
    }
}
