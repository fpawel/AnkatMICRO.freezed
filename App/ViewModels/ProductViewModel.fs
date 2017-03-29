namespace Ankat.ViewModel

open Ankat
open Pneumo

type Product(p, getProdType, getPgs, partyId) =

    inherit ViewModel.Product1(p, getProdType, getPgs, partyId) 
    override x.RaisePropertyChanged propertyName = 
        ViewModelBase.raisePropertyChanged x propertyName

    member x.Var_LIN0_0_CoutCh0
        with get () = x.getVarUi (LinPt(Sens1,Lin1), CoutCh0)
        and set value = x.setVarUi (LinPt(Sens1,Lin1), CoutCh0) value

    member x.Var_LIN0_1_CoutCh0
        with get () = x.getVarUi (LinPt(Sens1,Lin2), CoutCh0)
        and set value = x.setVarUi (LinPt(Sens1,Lin2), CoutCh0) value

    member x.Var_LIN0_2_CoutCh0
        with get () = x.getVarUi (LinPt(Sens1,Lin3), CoutCh0)
        and set value = x.setVarUi (LinPt(Sens1,Lin3), CoutCh0) value

    member x.Var_LIN0_3_CoutCh0
        with get () = x.getVarUi (LinPt(Sens1,Lin4), CoutCh0)
        and set value = x.setVarUi (LinPt(Sens1,Lin4), CoutCh0) value

    member x.Var_LIN1_0_CoutCh1
        with get () = x.getVarUi (LinPt(Sens2,Lin1), CoutCh1)
        and set value = x.setVarUi (LinPt(Sens2,Lin1), CoutCh1) value

    member x.Var_LIN1_1_CoutCh1
        with get () = x.getVarUi (LinPt(Sens2,Lin2), CoutCh1)
        and set value = x.setVarUi (LinPt(Sens2,Lin2), CoutCh1) value

    member x.Var_LIN1_2_CoutCh1
        with get () = x.getVarUi (LinPt(Sens2,Lin3), CoutCh1)
        and set value = x.setVarUi (LinPt(Sens2,Lin3), CoutCh1) value

    member x.Var_LIN1_3_CoutCh1
        with get () = x.getVarUi (LinPt(Sens2,Lin4), CoutCh1)
        and set value = x.setVarUi (LinPt(Sens2,Lin4), CoutCh1) value

    member x.Var_T00_0_TppCh0
        with get () = x.getVarUi (TermoScalePt(Sens1, ScaleBeg, TermoLow), TppCh0)
        and set value = x.setVarUi (TermoScalePt(Sens1, ScaleBeg, TermoLow), TppCh0) value

    member x.Var_T00_0_Var1Ch0
        with get () = x.getVarUi (TermoScalePt(Sens1, ScaleBeg, TermoLow), Var1Ch0)
        and set value = x.setVarUi (TermoScalePt(Sens1, ScaleBeg, TermoLow), Var1Ch0) value

    member x.Var_T00_1_TppCh0
        with get () = x.getVarUi (TermoScalePt(Sens1, ScaleBeg, TermoNorm), TppCh0)
        and set value = x.setVarUi (TermoScalePt(Sens1, ScaleBeg, TermoNorm), TppCh0) value

    member x.Var_T00_1_Var1Ch0
        with get () = x.getVarUi (TermoScalePt(Sens1, ScaleBeg, TermoNorm), Var1Ch0)
        and set value = x.setVarUi (TermoScalePt(Sens1, ScaleBeg, TermoNorm), Var1Ch0) value

    member x.Var_T00_2_TppCh0
        with get () = x.getVarUi (TermoScalePt(Sens1, ScaleBeg, TermoHigh), TppCh0)
        and set value = x.setVarUi (TermoScalePt(Sens1, ScaleBeg, TermoHigh), TppCh0) value

    member x.Var_T00_2_Var1Ch0
        with get () = x.getVarUi (TermoScalePt(Sens1, ScaleBeg, TermoHigh), Var1Ch0)
        and set value = x.setVarUi (TermoScalePt(Sens1, ScaleBeg, TermoHigh), Var1Ch0) value

    member x.Var_TK0_0_TppCh0
        with get () = x.getVarUi (TermoScalePt(Sens1, ScaleEnd, TermoLow), TppCh0)
        and set value = x.setVarUi (TermoScalePt(Sens1, ScaleEnd, TermoLow), TppCh0) value

    member x.Var_TK0_0_Var1Ch0
        with get () = x.getVarUi (TermoScalePt(Sens1, ScaleEnd, TermoLow), Var1Ch0)
        and set value = x.setVarUi (TermoScalePt(Sens1, ScaleEnd, TermoLow), Var1Ch0) value

    member x.Var_TK0_1_TppCh0
        with get () = x.getVarUi (TermoScalePt(Sens1, ScaleEnd, TermoNorm), TppCh0)
        and set value = x.setVarUi (TermoScalePt(Sens1, ScaleEnd, TermoNorm), TppCh0) value

    member x.Var_TK0_1_Var1Ch0
        with get () = x.getVarUi (TermoScalePt(Sens1, ScaleEnd, TermoNorm), Var1Ch0)
        and set value = x.setVarUi (TermoScalePt(Sens1, ScaleEnd, TermoNorm), Var1Ch0) value

    member x.Var_TK0_2_TppCh0
        with get () = x.getVarUi (TermoScalePt(Sens1, ScaleEnd, TermoHigh), TppCh0)
        and set value = x.setVarUi (TermoScalePt(Sens1, ScaleEnd, TermoHigh), TppCh0) value

    member x.Var_TK0_2_Var1Ch0
        with get () = x.getVarUi (TermoScalePt(Sens1, ScaleEnd, TermoHigh), Var1Ch0)
        and set value = x.setVarUi (TermoScalePt(Sens1, ScaleEnd, TermoHigh), Var1Ch0) value

    member x.Var_T01_0_TppCh1
        with get () = x.getVarUi (TermoScalePt(Sens2, ScaleBeg, TermoLow), TppCh1)
        and set value = x.setVarUi (TermoScalePt(Sens2, ScaleBeg, TermoLow), TppCh1) value

    member x.Var_T01_0_Var1Ch1
        with get () = x.getVarUi (TermoScalePt(Sens2, ScaleBeg, TermoLow), Var1Ch1)
        and set value = x.setVarUi (TermoScalePt(Sens2, ScaleBeg, TermoLow), Var1Ch1) value

    member x.Var_T01_1_TppCh1
        with get () = x.getVarUi (TermoScalePt(Sens2, ScaleBeg, TermoNorm), TppCh1)
        and set value = x.setVarUi (TermoScalePt(Sens2, ScaleBeg, TermoNorm), TppCh1) value

    member x.Var_T01_1_Var1Ch1
        with get () = x.getVarUi (TermoScalePt(Sens2, ScaleBeg, TermoNorm), Var1Ch1)
        and set value = x.setVarUi (TermoScalePt(Sens2, ScaleBeg, TermoNorm), Var1Ch1) value

    member x.Var_T01_2_TppCh1
        with get () = x.getVarUi (TermoScalePt(Sens2, ScaleBeg, TermoHigh), TppCh1)
        and set value = x.setVarUi (TermoScalePt(Sens2, ScaleBeg, TermoHigh), TppCh1) value

    member x.Var_T01_2_Var1Ch1
        with get () = x.getVarUi (TermoScalePt(Sens2, ScaleBeg, TermoHigh), Var1Ch1)
        and set value = x.setVarUi (TermoScalePt(Sens2, ScaleBeg, TermoHigh), Var1Ch1) value

    member x.Var_TK1_0_TppCh1
        with get () = x.getVarUi (TermoScalePt(Sens2, ScaleEnd, TermoLow), TppCh1)
        and set value = x.setVarUi (TermoScalePt(Sens2, ScaleEnd, TermoLow), TppCh1) value

    member x.Var_TK1_0_Var1Ch1
        with get () = x.getVarUi (TermoScalePt(Sens2, ScaleEnd, TermoLow), Var1Ch1)
        and set value = x.setVarUi (TermoScalePt(Sens2, ScaleEnd, TermoLow), Var1Ch1) value

    member x.Var_TK1_1_TppCh1
        with get () = x.getVarUi (TermoScalePt(Sens2, ScaleEnd, TermoNorm), TppCh1)
        and set value = x.setVarUi (TermoScalePt(Sens2, ScaleEnd, TermoNorm), TppCh1) value

    member x.Var_TK1_1_Var1Ch1
        with get () = x.getVarUi (TermoScalePt(Sens2, ScaleEnd, TermoNorm), Var1Ch1)
        and set value = x.setVarUi (TermoScalePt(Sens2, ScaleEnd, TermoNorm), Var1Ch1) value

    member x.Var_TK1_2_TppCh1
        with get () = x.getVarUi (TermoScalePt(Sens2, ScaleEnd, TermoHigh), TppCh1)
        and set value = x.setVarUi (TermoScalePt(Sens2, ScaleEnd, TermoHigh), TppCh1) value

    member x.Var_TK1_2_Var1Ch1
        with get () = x.getVarUi (TermoScalePt(Sens2, ScaleEnd, TermoHigh), Var1Ch1)
        and set value = x.setVarUi (TermoScalePt(Sens2, ScaleEnd, TermoHigh), Var1Ch1) value

    member x.Var_PT0_TppCh0
        with get () = x.getVarUi (TermoPressPt(TermoLow), TppCh0)
        and set value = x.setVarUi (TermoPressPt(TermoLow), TppCh0) value

    member x.Var_PT0_VdatP
        with get () = x.getVarUi (TermoPressPt(TermoLow), VdatP)
        and set value = x.setVarUi (TermoPressPt(TermoLow), VdatP) value

    member x.Var_PT1_TppCh0
        with get () = x.getVarUi (TermoPressPt(TermoNorm), TppCh0)
        and set value = x.setVarUi (TermoPressPt(TermoNorm), TppCh0) value

    member x.Var_PT1_VdatP
        with get () = x.getVarUi (TermoPressPt(TermoNorm), VdatP)
        and set value = x.setVarUi (TermoPressPt(TermoNorm), VdatP) value

    member x.Var_PT2_TppCh0
        with get () = x.getVarUi (TermoPressPt(TermoHigh), TppCh0)
        and set value = x.setVarUi (TermoPressPt(TermoHigh), TppCh0) value

    member x.Var_PT2_VdatP
        with get () = x.getVarUi (TermoPressPt(TermoHigh), VdatP)
        and set value = x.setVarUi (TermoPressPt(TermoHigh), VdatP) value

    member x.Var_PS0_Pmm
        with get () = x.getVarUi (PressSensPt(PressNorm), Pmm)
        and set value = x.setVarUi (PressSensPt(PressNorm), Pmm) value

    member x.Var_PS0_VdatP
        with get () = x.getVarUi (PressSensPt(PressNorm), VdatP)
        and set value = x.setVarUi (PressSensPt(PressNorm), VdatP) value

    member x.Var_PS1_Pmm
        with get () = x.getVarUi (PressSensPt(PressHigh), Pmm)
        and set value = x.setVarUi (PressSensPt(PressHigh), Pmm) value

    member x.Var_PS1_VdatP
        with get () = x.getVarUi (PressSensPt(PressHigh), VdatP)
        and set value = x.setVarUi (PressSensPt(PressHigh), VdatP) value

    member x.Var_TEST0_0_TppCh0
        with get () = x.getVarUi (TestPt(Sens1, Test1Gas1), TppCh0)
        and set value = x.setVarUi (TestPt(Sens1, Test1Gas1), TppCh0) value

    member x.Var_TEST0_0_CoutCh0
        with get () = x.getVarUi (TestPt(Sens1, Test1Gas1), CoutCh0)
        and set value = x.setVarUi (TestPt(Sens1, Test1Gas1), CoutCh0) value

    member x.Var_TEST0_1_TppCh0
        with get () = x.getVarUi (TestPt(Sens1, Test2Gas2), TppCh0)
        and set value = x.setVarUi (TestPt(Sens1, Test2Gas2), TppCh0) value

    member x.Var_TEST0_1_CoutCh0
        with get () = x.getVarUi (TestPt(Sens1, Test2Gas2), CoutCh0)
        and set value = x.setVarUi (TestPt(Sens1, Test2Gas2), CoutCh0) value

    member x.Var_TEST0_2_TppCh0
        with get () = x.getVarUi (TestPt(Sens1, Test3Gas3), TppCh0)
        and set value = x.setVarUi (TestPt(Sens1, Test3Gas3), TppCh0) value

    member x.Var_TEST0_2_CoutCh0
        with get () = x.getVarUi (TestPt(Sens1, Test3Gas3), CoutCh0)
        and set value = x.setVarUi (TestPt(Sens1, Test3Gas3), CoutCh0) value

    member x.Var_TEST0_3_TppCh0
        with get () = x.getVarUi (TestPt(Sens1, Test4Gas2), TppCh0)
        and set value = x.setVarUi (TestPt(Sens1, Test4Gas2), TppCh0) value

    member x.Var_TEST0_3_CoutCh0
        with get () = x.getVarUi (TestPt(Sens1, Test4Gas2), CoutCh0)
        and set value = x.setVarUi (TestPt(Sens1, Test4Gas2), CoutCh0) value

    member x.Var_TEST0_4_TppCh0
        with get () = x.getVarUi (TestPt(Sens1, Test5Gas1), TppCh0)
        and set value = x.setVarUi (TestPt(Sens1, Test5Gas1), TppCh0) value

    member x.Var_TEST0_4_CoutCh0
        with get () = x.getVarUi (TestPt(Sens1, Test5Gas1), CoutCh0)
        and set value = x.setVarUi (TestPt(Sens1, Test5Gas1), CoutCh0) value

    member x.Var_TEST0_5_TppCh0
        with get () = x.getVarUi (TestPt(Sens1, Test6Gas3), TppCh0)
        and set value = x.setVarUi (TestPt(Sens1, Test6Gas3), TppCh0) value

    member x.Var_TEST0_5_CoutCh0
        with get () = x.getVarUi (TestPt(Sens1, Test6Gas3), CoutCh0)
        and set value = x.setVarUi (TestPt(Sens1, Test6Gas3), CoutCh0) value

    member x.Var_TEST1_0_TppCh1
        with get () = x.getVarUi (TestPt(Sens2, Test1Gas1), TppCh1)
        and set value = x.setVarUi (TestPt(Sens2, Test1Gas1), TppCh1) value

    member x.Var_TEST1_0_CoutCh1
        with get () = x.getVarUi (TestPt(Sens2, Test1Gas1), CoutCh1)
        and set value = x.setVarUi (TestPt(Sens2, Test1Gas1), CoutCh1) value

    member x.Var_TEST1_1_TppCh1
        with get () = x.getVarUi (TestPt(Sens2, Test2Gas2), TppCh1)
        and set value = x.setVarUi (TestPt(Sens2, Test2Gas2), TppCh1) value

    member x.Var_TEST1_1_CoutCh1
        with get () = x.getVarUi (TestPt(Sens2, Test2Gas2), CoutCh1)
        and set value = x.setVarUi (TestPt(Sens2, Test2Gas2), CoutCh1) value

    member x.Var_TEST1_2_TppCh1
        with get () = x.getVarUi (TestPt(Sens2, Test3Gas3), TppCh1)
        and set value = x.setVarUi (TestPt(Sens2, Test3Gas3), TppCh1) value

    member x.Var_TEST1_2_CoutCh1
        with get () = x.getVarUi (TestPt(Sens2, Test3Gas3), CoutCh1)
        and set value = x.setVarUi (TestPt(Sens2, Test3Gas3), CoutCh1) value

    member x.Var_TEST1_3_TppCh1
        with get () = x.getVarUi (TestPt(Sens2, Test4Gas2), TppCh1)
        and set value = x.setVarUi (TestPt(Sens2, Test4Gas2), TppCh1) value

    member x.Var_TEST1_3_CoutCh1
        with get () = x.getVarUi (TestPt(Sens2, Test4Gas2), CoutCh1)
        and set value = x.setVarUi (TestPt(Sens2, Test4Gas2), CoutCh1) value

    member x.Var_TEST1_4_TppCh1
        with get () = x.getVarUi (TestPt(Sens2, Test5Gas1), TppCh1)
        and set value = x.setVarUi (TestPt(Sens2, Test5Gas1), TppCh1) value

    member x.Var_TEST1_4_CoutCh1
        with get () = x.getVarUi (TestPt(Sens2, Test5Gas1), CoutCh1)
        and set value = x.setVarUi (TestPt(Sens2, Test5Gas1), CoutCh1) value

    member x.Var_TEST1_5_TppCh1
        with get () = x.getVarUi (TestPt(Sens2, Test6Gas3), TppCh1)
        and set value = x.setVarUi (TestPt(Sens2, Test6Gas3), TppCh1) value

    member x.Var_TEST1_5_CoutCh1
        with get () = x.getVarUi (TestPt(Sens2, Test6Gas3), CoutCh1)
        and set value = x.setVarUi (TestPt(Sens2, Test6Gas3), CoutCh1) value

    member x.ConcError0_0 = x.GetConcError (Sens1,Test1Gas1)

    member x.ConcError1_0 = x.GetConcError (Sens2,Test1Gas1)

    member x.ConcError0_1 = x.GetConcError (Sens1,Test2Gas2)

    member x.ConcError1_1 = x.GetConcError (Sens2,Test2Gas2)

    member x.ConcError0_2 = x.GetConcError (Sens1,Test3Gas3)

    member x.ConcError1_2 = x.GetConcError (Sens2,Test3Gas3)

    member x.ConcError0_3 = x.GetConcError (Sens1,Test4Gas2)

    member x.ConcError1_3 = x.GetConcError (Sens2,Test4Gas2)

    member x.ConcError0_4 = x.GetConcError (Sens1,Test5Gas1)

    member x.ConcError1_4 = x.GetConcError (Sens2,Test5Gas1)

    member x.ConcError0_5 = x.GetConcError (Sens1,Test6Gas3)

    member x.ConcError1_5 = x.GetConcError (Sens2,Test6Gas3)

    member x.CCh0 = x.getPhysVarValueUi(CCh0)

    member x.CCh1 = x.getPhysVarValueUi(CCh1)

    member x.CCh2 = x.getPhysVarValueUi(CCh2)

    member x.PkPa = x.getPhysVarValueUi(PkPa)

    member x.Pmm = x.getPhysVarValueUi(Pmm)

    member x.Tmcu = x.getPhysVarValueUi(Tmcu)

    member x.Vbat = x.getPhysVarValueUi(Vbat)

    member x.Vref = x.getPhysVarValueUi(Vref)

    member x.Vmcu = x.getPhysVarValueUi(Vmcu)

    member x.VdatP = x.getPhysVarValueUi(VdatP)

    member x.CoutCh0 = x.getPhysVarValueUi(CoutCh0)

    member x.TppCh0 = x.getPhysVarValueUi(TppCh0)

    member x.ILOn0 = x.getPhysVarValueUi(ILOn0)

    member x.ILOff0 = x.getPhysVarValueUi(ILOff0)

    member x.Uw_Ch0 = x.getPhysVarValueUi(Uw_Ch0)

    member x.Ur_Ch0 = x.getPhysVarValueUi(Ur_Ch0)

    member x.WORK0 = x.getPhysVarValueUi(WORK0)

    member x.REF0 = x.getPhysVarValueUi(REF0)

    member x.Var1Ch0 = x.getPhysVarValueUi(Var1Ch0)

    member x.Var2Ch0 = x.getPhysVarValueUi(Var2Ch0)

    member x.Var3Ch0 = x.getPhysVarValueUi(Var3Ch0)

    member x.FppCh0 = x.getPhysVarValueUi(FppCh0)

    member x.CoutCh1 = x.getPhysVarValueUi(CoutCh1)

    member x.TppCh1 = x.getPhysVarValueUi(TppCh1)

    member x.ILOn1 = x.getPhysVarValueUi(ILOn1)

    member x.ILOff1 = x.getPhysVarValueUi(ILOff1)

    member x.Uw_Ch1 = x.getPhysVarValueUi(Uw_Ch1)

    member x.Ur_Ch1 = x.getPhysVarValueUi(Ur_Ch1)

    member x.WORK1 = x.getPhysVarValueUi(WORK1)

    member x.REF1 = x.getPhysVarValueUi(REF1)

    member x.Var1Ch1 = x.getPhysVarValueUi(Var1Ch1)

    member x.Var2Ch1 = x.getPhysVarValueUi(Var2Ch1)

    member x.Var3Ch1 = x.getPhysVarValueUi(Var3Ch1)

    member x.FppCh1 = x.getPhysVarValueUi(FppCh1)
