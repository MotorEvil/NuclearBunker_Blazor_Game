﻿using Microsoft.AspNetCore.Components;
using NuclearWinter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NuclearWinter.Pages.SFW_Lt_Version
{
    public partial class SFW_Lt_version : ComponentBase
    {
        private AbilitieLt[] abilities;
        private PersonLt[] persons;

        private readonly List<string> personList = new List<string>();
        private readonly List<string> abilitiesList = new List<string>();
        private readonly List<GamePerson> gamePerson = new List<GamePerson>();
        private Random rng = new Random();
        private TimeSpan TimeLeft = new TimeSpan();
        private string TimeOutMessage, TimeOutMessage2;
        private bool GameEnded = false;
        private int hours = 0;
        private int min = 0;
        private int sec = 0;
        private bool isTimePicked = false;
        private bool isGameStarted = false;
        private bool NoTimeIsPicked = false;
        private string FiveSecCSS = "btn btn-primary", 
            OneMinuteCSS = "btn btn-primary",
            FiveMinutesCSS = "btn btn-primary",
            NoTimeCSS = "btn btn-primary", 
            TenMinutesCSS = "btn btn-primary";
        private string TimeCss;

        protected override async Task OnInitializedAsync()
        {
            abilities = await Http.GetFromJsonAsync<AbilitieLt[]>("sample-data/AbilitiesLt.json");
            persons = await Http.GetFromJsonAsync<PersonLt[]>("sample-data/personLt.json");

            foreach (var abilitie in abilities)
            {
                abilitiesList.Add(abilitie.Abilitie);
            }

            foreach (var person in persons)
            {
                personList.Add(person.Name);
            }
        }

        private void TenMinutes()
        {
            hours = 0;
            min = 10;
            sec = 0;
            isTimePicked = true;
            FiveSecCSS = "btn btn-primary";
            OneMinuteCSS = "btn btn-primary";
            FiveMinutesCSS = "btn btn-primary"; 
            TenMinutesCSS = "btn btn-info";
            NoTimeCSS = "btn btn-primary";
            TimeLeft = new TimeSpan(hours, min, sec);
        }

        private void FiveMinutes()
        {
            hours = 0;
            min = 5;
            sec = 0;
            isTimePicked = true;
            FiveSecCSS = "btn btn-primary";
            OneMinuteCSS = "btn btn-primary";
            FiveMinutesCSS = "btn btn-info";
            TenMinutesCSS = "btn btn-primary";
            NoTimeCSS = "btn btn-primary";
            TimeLeft = new TimeSpan(hours, min, sec);
        }

        private void OneMinute()
        {
            hours = 0;
            min = 1;
            sec = 0;
            isTimePicked = true;
            FiveSecCSS = "btn btn-primary";
            OneMinuteCSS = "btn btn-info";
            FiveMinutesCSS = "btn btn-primary";
            TenMinutesCSS = "btn btn-primary";
            NoTimeCSS = "btn btn-primary";
            TimeLeft = new TimeSpan(hours, min, sec);
        }

        private void FiveSeconds()
        {
            hours = 0;
            min = 0;
            sec = 5;
            isTimePicked = true;
            FiveSecCSS = "btn btn-info";
            OneMinuteCSS = "btn btn-primary";
            FiveMinutesCSS = "btn btn-primary";
            TenMinutesCSS = "btn btn-primary";
            NoTimeCSS = "btn btn-primary";
            TimeLeft = new TimeSpan(hours, min, sec);
        }

        private void NoTime()
        {
            isTimePicked = false;
            NoTimeIsPicked = true;
            FiveSecCSS = "btn btn-primary";
            OneMinuteCSS = "btn btn-primary";
            FiveMinutesCSS = "btn btn-primary";
            TenMinutesCSS = "btn btn-primary";
            NoTimeCSS = "btn btn-info";
        }

        private void Game()
        {
            Timer();
            isGameStarted = true;

            while (gamePerson.Count() < 10)
            {
                int Personindex = rng.Next(0, personList.Count() - 1);
                int AbilitieIndex = rng.Next(0, abilitiesList.Count() - 1);

                string person = $"{personList[Personindex].ToUpper()}, kuris {abilitiesList[AbilitieIndex]}.";
                gamePerson.Add(new GamePerson()
                {
                    FullGamePerson = person
                });

                personList.RemoveAt(Personindex);
                abilitiesList.RemoveAt(AbilitieIndex);
            }
        }

        private async Task Timer()
        {
            while (TimeLeft > new TimeSpan())
            {
                await Task.Delay(1000);
                TimeLeft = TimeLeft.Subtract(new TimeSpan(0, 0, 1));
                if (TimeLeft < new TimeSpan(0, 0, 30))
                {
                    TimeCss = "color:red";
                }
                StateHasChanged();
            }
            if (isTimePicked != false)
            {
                await AfterTime();
                StateHasChanged();
            }
        }

        private Task AfterTime()
        {
            TimeOutMessage = "Nespėjot laiku išsirinkti komandos!!";
            TimeOutMessage2 = "Susivieniję paklydėliai Jus išmetė iš bunkerio!!";
            isGameStarted = false;
            GameEnded = true;
            return Task.CompletedTask;
        }

        private void NewGame()
        {
            NavigationManager.NavigateTo("refreshSFWlt");
        }
    }
}