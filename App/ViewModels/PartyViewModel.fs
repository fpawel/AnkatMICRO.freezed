namespace Ankat.ViewModel
open System
open System.ComponentModel
open Ankat
open Pneumo

type Party(partyHeader, partyData) =

    inherit ViewModel.Party1(partyHeader, partyData) 
    override x.RaisePropertyChanged propertyName = 
        ViewModelBase.raisePropertyChanged x propertyName

