using Microsoft.AspNetCore.Components;
using NuclearWinter.Models;
using System;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NuclearWinter.Pages.SFW_Lt_Version
{
    public partial class SFW_Lt_version : ComponentBase
    {
        private Random rng = new Random();
        private TimeSpan TimeLeft = new TimeSpan();
        private string EndGameMessage, EndGameMessage2, EndGameMessageCSS, TimeCss;
        private bool GameEnded, isTimePicked, isGameStarted, NoTimeIsPicked, IsSubmited;
        private int hours, min, sec;

        private string FiveSecCSS = "btn btn-primary",
            OneMinuteCSS = "btn btn-primary",
            FiveMinutesCSS = "btn btn-primary",
            NoTimeCSS = "btn btn-primary",
            TenMinutesCSS = "btn btn-primary";

        protected override async Task OnInitializedAsync()
        {
            context.abilitiesLt = await Http.GetFromJsonAsync<AbilitieLt[]>("sample-data/AbilitiesLt.json");
            context.personsLt = await Http.GetFromJsonAsync<PersonLt[]>("sample-data/personLt.json");

            foreach (var abilitie in context.abilitiesLt)
            {
                context.abilitiesList.Add(new AbilitieLt()
                {
                    Abilitie = abilitie.Abilitie
                });
            }

            foreach (var person in context.personsLt)
            {
                context.personList.Add(new PersonLt()
                {
                    Name = person.Name,
                    Gender = person.Gender
                });
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

            while (context.gamePerson.Count() < 10)
            {
                int PersonIndex = rng.Next(0, context.personList.Count() - 1);
                int AbilitieIndex = rng.Next(0, context.abilitiesList.Count() - 1);
                string which = "kuris";
                string Female = "Female";

                if (context.personList[PersonIndex].Gender == Female)
                {
                    which = "kuri";
                }
                string person = $"{context.personList[PersonIndex].Name.ToUpper()}, {which} {context.abilitiesList[AbilitieIndex].Abilitie}.";
                context.gamePerson.Add(new GamePerson()
                {
                    FullGamePerson = person
                });

                context.personList.RemoveAt(PersonIndex);
                context.abilitiesList.RemoveAt(AbilitieIndex);
            }
        }

        private async Task Timer()
        {
            while (TimeLeft > new TimeSpan() && IsSubmited == false)
            {
                await Task.Delay(1000);
                TimeLeft = TimeLeft.Subtract(new TimeSpan(0, 0, 1));
                if (TimeLeft < new TimeSpan(0, 0, 30))
                {
                    TimeCss = "color:red";
                }
                StateHasChanged();
            }
            if (isTimePicked != false && IsSubmited == false)
            {
                await AfterTime();
                StateHasChanged();
            }
        }

        private Task AfterTime()
        {
            EndGameMessage = "Nespėjot laiku išsirinkti komandos!!";
            EndGameMessage2 = "Susivieniję paklydėliai Jus išmetė iš bunkerio!!";
            EndGameMessageCSS = "color:red";
            isGameStarted = false;
            GameEnded = true;
            return Task.CompletedTask;
        }

        private void NewGame()
        {
            Array.Clear(context.abilitiesLt, 0, context.abilitiesLt.Length);
            Array.Clear(context.personsLt, 0, context.personsLt.Length);
            context.gamePerson.Clear();
            context.abilitiesList.Clear();
            context.personList.Clear();
            NavigationManager.NavigateTo("refreshSFWlt");
        }

        private void SubmitChoises()
        {
            isGameStarted = false;
            GameEnded = true;
            IsSubmited = true;
            EndGameMessage = "Sveikinam išsirinkus savo naujus draugus!!!";
            EndGameMessageCSS = "color:blue";
        }
    }
}