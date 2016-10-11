namespace Ankat.ViewModel

open Ankat

type Product(p, getProdType, getPgs, partyId) =

    inherit ViewModel.Product1(p, getProdType, getPgs, partyId) 
    override x.RaisePropertyChanged propertyName = 
        ViewModelBase.raisePropertyChanged x propertyName

    member x.Var_GroupCoefs_Lin_Sens1_CCh0_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens1)), PhysVar.CCh0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens1)), PhysVar.CCh0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Lin_Sens1_CCh0_ScaleMid1_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens1)), PhysVar.CCh0, ScalePt.ScaleMid1, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens1)), PhysVar.CCh0, ScalePt.ScaleMid1, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Lin_Sens1_CCh0_ScaleMid2_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens1)), PhysVar.CCh0, ScalePt.ScaleMid2, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens1)), PhysVar.CCh0, ScalePt.ScaleMid2, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Lin_Sens1_CCh0_ScaleEnd_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens1)), PhysVar.CCh0, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens1)), PhysVar.CCh0, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_CCh0_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_CCh0_ScaleMid1_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleMid1, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleMid1, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_CCh0_ScaleMid2_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleMid2, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleMid2, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_CCh0_ScaleEnd_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_CCh0_ScaleBeg_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_CCh0_ScaleMid1_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleMid1, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleMid1, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_CCh0_ScaleMid2_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleMid2, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleMid2, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_CCh0_ScaleEnd_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_CCh0_ScaleBeg_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_Test_CCh0_ScaleMid1_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleMid1, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleMid1, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_Test_CCh0_ScaleMid2_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleMid2, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleMid2, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_Test_CCh0_ScaleEnd_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh0, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Lin_Sens2_CCh1_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens2)), PhysVar.CCh1, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens2)), PhysVar.CCh1, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Lin_Sens2_CCh1_ScaleMid1_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens2)), PhysVar.CCh1, ScalePt.ScaleMid1, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens2)), PhysVar.CCh1, ScalePt.ScaleMid1, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Lin_Sens2_CCh1_ScaleEnd_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens2)), PhysVar.CCh1, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.LinCoefs(SensorIndex.Sens2)), PhysVar.CCh1, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_CCh1_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_CCh1_ScaleMid1_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleMid1, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleMid1, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_CCh1_ScaleEnd_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_CCh1_ScaleBeg_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_CCh1_ScaleMid1_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleMid1, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleMid1, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_CCh1_ScaleEnd_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_CCh1_ScaleBeg_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_Test_CCh1_ScaleMid1_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleMid1, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleMid1, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_Test_CCh1_ScaleEnd_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.CCh1, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_TermoPressureCoefs_Pmm_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.Pmm, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.Pmm, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_TermoPressureCoefs_Pmm_ScaleBeg_TermoNorm_P800
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.Pmm, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Phigh)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.Pmm, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Phigh) value

    member x.Var_GroupCoefs_TermoPressureCoefs_Pmm_ScaleBeg_TermoLow_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.Pmm, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.Pmm, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_TermoPressureCoefs_Pmm_ScaleBeg_TermoLow_P800
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.Pmm, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Phigh)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.Pmm, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Phigh) value

    member x.Var_GroupCoefs_TermoPressureCoefs_Pmm_ScaleBeg_TermoHigh_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.Pmm, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.Pmm, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_TermoPressureCoefs_Pmm_ScaleBeg_TermoHigh_P800
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.Pmm, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Phigh)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.Pmm, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Phigh) value

    member x.Var_GroupCoefs_TermoPressureCoefs_VdatP_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_TermoPressureCoefs_VdatP_ScaleBeg_TermoNorm_P800
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Phigh)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Phigh) value

    member x.Var_GroupCoefs_TermoPressureCoefs_VdatP_ScaleBeg_TermoLow_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_TermoPressureCoefs_VdatP_ScaleBeg_TermoLow_P800
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Phigh)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Phigh) value

    member x.Var_GroupCoefs_TermoPressureCoefs_VdatP_ScaleBeg_TermoHigh_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_TermoPressureCoefs_VdatP_ScaleBeg_TermoHigh_P800
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Phigh)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoPressureCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Phigh) value

    member x.Var_GroupCoefs_PressureSensCoefs_VdatP_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_PressureSensCoefs_VdatP_ScaleBeg_TermoNorm_P800
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Phigh)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Phigh) value

    member x.Var_GroupCoefs_PressureSensCoefs_VdatP_ScaleBeg_TermoLow_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_PressureSensCoefs_VdatP_ScaleBeg_TermoLow_P800
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Phigh)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Phigh) value

    member x.Var_GroupCoefs_PressureSensCoefs_VdatP_ScaleBeg_TermoHigh_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_PressureSensCoefs_VdatP_ScaleBeg_TermoHigh_P800
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Phigh)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.VdatP, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Phigh) value

    member x.Var_GroupCoefs_Termo_Sens1_ScaleBeg_TppCh0_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleBeg)), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleBeg)), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens1_ScaleBeg_TppCh0_ScaleBeg_TermoLow_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleBeg)), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleBeg)), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens1_ScaleBeg_TppCh0_ScaleBeg_TermoHigh_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleBeg)), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleBeg)), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens1_ScaleEnd_TppCh0_ScaleEnd_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleEnd)), PhysVar.TppCh0, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleEnd)), PhysVar.TppCh0, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens1_ScaleEnd_TppCh0_ScaleEnd_TermoLow_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleEnd)), PhysVar.TppCh0, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleEnd)), PhysVar.TppCh0, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens1_ScaleEnd_TppCh0_ScaleEnd_TermoHigh_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleEnd)), PhysVar.TppCh0, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleEnd)), PhysVar.TppCh0, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_PressureSensCoefs_TppCh0_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_PressureSensCoefs_TppCh0_ScaleBeg_TermoNorm_P800
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Phigh)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Phigh) value

    member x.Var_GroupCoefs_PressureSensCoefs_TppCh0_ScaleBeg_TermoLow_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_PressureSensCoefs_TppCh0_ScaleBeg_TermoLow_P800
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Phigh)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Phigh) value

    member x.Var_GroupCoefs_PressureSensCoefs_TppCh0_ScaleBeg_TermoHigh_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_PressureSensCoefs_TppCh0_ScaleBeg_TermoHigh_P800
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Phigh)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.PressureSensCoefs), PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Phigh) value

    member x.Var_Test_TppCh0_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_TppCh0_ScaleMid1_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleMid1, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleMid1, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_TppCh0_ScaleMid2_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleMid2, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleMid2, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_TppCh0_ScaleEnd_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_TppCh0_ScaleBeg_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_TppCh0_ScaleMid1_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleMid1, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleMid1, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_TppCh0_ScaleMid2_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleMid2, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleMid2, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_TppCh0_ScaleEnd_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_TppCh0_ScaleBeg_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_Test_TppCh0_ScaleMid1_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleMid1, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleMid1, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_Test_TppCh0_ScaleMid2_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleMid2, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleMid2, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_Test_TppCh0_ScaleEnd_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh0, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens1_ScaleBeg_Var1Ch0_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleBeg)), PhysVar.Var1Ch0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleBeg)), PhysVar.Var1Ch0, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens1_ScaleBeg_Var1Ch0_ScaleBeg_TermoLow_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleBeg)), PhysVar.Var1Ch0, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleBeg)), PhysVar.Var1Ch0, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens1_ScaleBeg_Var1Ch0_ScaleBeg_TermoHigh_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleBeg)), PhysVar.Var1Ch0, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleBeg)), PhysVar.Var1Ch0, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens1_ScaleEnd_Var1Ch0_ScaleEnd_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleEnd)), PhysVar.Var1Ch0, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleEnd)), PhysVar.Var1Ch0, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens1_ScaleEnd_Var1Ch0_ScaleEnd_TermoLow_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleEnd)), PhysVar.Var1Ch0, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleEnd)), PhysVar.Var1Ch0, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens1_ScaleEnd_Var1Ch0_ScaleEnd_TermoHigh_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleEnd)), PhysVar.Var1Ch0, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens1, ScalePt.ScaleEnd)), PhysVar.Var1Ch0, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens2_ScaleBeg_TppCh1_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleBeg)), PhysVar.TppCh1, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleBeg)), PhysVar.TppCh1, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens2_ScaleBeg_TppCh1_ScaleBeg_TermoLow_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleBeg)), PhysVar.TppCh1, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleBeg)), PhysVar.TppCh1, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens2_ScaleBeg_TppCh1_ScaleBeg_TermoHigh_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleBeg)), PhysVar.TppCh1, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleBeg)), PhysVar.TppCh1, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens2_ScaleEnd_TppCh1_ScaleEnd_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleEnd)), PhysVar.TppCh1, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleEnd)), PhysVar.TppCh1, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens2_ScaleEnd_TppCh1_ScaleEnd_TermoLow_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleEnd)), PhysVar.TppCh1, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleEnd)), PhysVar.TppCh1, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens2_ScaleEnd_TppCh1_ScaleEnd_TermoHigh_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleEnd)), PhysVar.TppCh1, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleEnd)), PhysVar.TppCh1, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_Test_TppCh1_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_TppCh1_ScaleMid1_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleMid1, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleMid1, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_TppCh1_ScaleEnd_TermoNorm_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_Test_TppCh1_ScaleBeg_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_TppCh1_ScaleMid1_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleMid1, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleMid1, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_TppCh1_ScaleEnd_TermoLow_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_Test_TppCh1_ScaleBeg_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_Test_TppCh1_ScaleMid1_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleMid1, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleMid1, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_Test_TppCh1_ScaleEnd_TermoHigh_P740
        with get () = x.getVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.Test, PhysVar.TppCh1, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens2_ScaleBeg_Var1Ch1_ScaleBeg_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleBeg)), PhysVar.Var1Ch1, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleBeg)), PhysVar.Var1Ch1, ScalePt.ScaleBeg, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens2_ScaleBeg_Var1Ch1_ScaleBeg_TermoLow_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleBeg)), PhysVar.Var1Ch1, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleBeg)), PhysVar.Var1Ch1, ScalePt.ScaleBeg, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens2_ScaleBeg_Var1Ch1_ScaleBeg_TermoHigh_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleBeg)), PhysVar.Var1Ch1, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleBeg)), PhysVar.Var1Ch1, ScalePt.ScaleBeg, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens2_ScaleEnd_Var1Ch1_ScaleEnd_TermoNorm_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleEnd)), PhysVar.Var1Ch1, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleEnd)), PhysVar.Var1Ch1, ScalePt.ScaleEnd, TermoPt.TermoNorm, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens2_ScaleEnd_Var1Ch1_ScaleEnd_TermoLow_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleEnd)), PhysVar.Var1Ch1, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleEnd)), PhysVar.Var1Ch1, ScalePt.ScaleEnd, TermoPt.TermoLow, PressurePt.Pnorm) value

    member x.Var_GroupCoefs_Termo_Sens2_ScaleEnd_Var1Ch1_ScaleEnd_TermoHigh_P740
        with get () = x.getVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleEnd)), PhysVar.Var1Ch1, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm)
        and set value = x.setVarUi (Feature.FeatureKefGroup(GroupCoefs.TermoCoefs(SensorIndex.Sens2, ScalePt.ScaleEnd)), PhysVar.Var1Ch1, ScalePt.ScaleEnd, TermoPt.TermoHigh, PressurePt.Pnorm) value

    member x.ConcError_Sens1_ScaleBeg = x.GetConcError (SensorIndex.Sens1, ScalePt.ScaleBeg) 

    member x.ConcError_Sens1_ScaleMid1 = x.GetConcError (SensorIndex.Sens1, ScalePt.ScaleMid1) 

    member x.ConcError_Sens1_ScaleMid2 = x.GetConcError (SensorIndex.Sens1, ScalePt.ScaleMid2) 

    member x.ConcError_Sens1_ScaleEnd = x.GetConcError (SensorIndex.Sens1, ScalePt.ScaleEnd) 

    member x.ConcError_Sens2_ScaleBeg = x.GetConcError (SensorIndex.Sens2, ScalePt.ScaleBeg) 

    member x.ConcError_Sens2_ScaleMid1 = x.GetConcError (SensorIndex.Sens2, ScalePt.ScaleMid1) 

    member x.ConcError_Sens2_ScaleEnd = x.GetConcError (SensorIndex.Sens2, ScalePt.ScaleEnd) 

    member x.TermoError_Sens1_ScaleBeg_TermoNorm = x.GetTermoError (SensorIndex.Sens1, ScalePt.ScaleBeg, TermoPt.TermoNorm) 

    member x.TermoError_Sens1_ScaleBeg_TermoLow = x.GetTermoError (SensorIndex.Sens1, ScalePt.ScaleBeg, TermoPt.TermoLow) 

    member x.TermoError_Sens1_ScaleBeg_TermoHigh = x.GetTermoError (SensorIndex.Sens1, ScalePt.ScaleBeg, TermoPt.TermoHigh) 

    member x.TermoError_Sens1_ScaleMid1_TermoNorm = x.GetTermoError (SensorIndex.Sens1, ScalePt.ScaleMid1, TermoPt.TermoNorm) 

    member x.TermoError_Sens1_ScaleMid1_TermoLow = x.GetTermoError (SensorIndex.Sens1, ScalePt.ScaleMid1, TermoPt.TermoLow) 

    member x.TermoError_Sens1_ScaleMid1_TermoHigh = x.GetTermoError (SensorIndex.Sens1, ScalePt.ScaleMid1, TermoPt.TermoHigh) 

    member x.TermoError_Sens1_ScaleMid2_TermoNorm = x.GetTermoError (SensorIndex.Sens1, ScalePt.ScaleMid2, TermoPt.TermoNorm) 

    member x.TermoError_Sens1_ScaleMid2_TermoLow = x.GetTermoError (SensorIndex.Sens1, ScalePt.ScaleMid2, TermoPt.TermoLow) 

    member x.TermoError_Sens1_ScaleMid2_TermoHigh = x.GetTermoError (SensorIndex.Sens1, ScalePt.ScaleMid2, TermoPt.TermoHigh) 

    member x.TermoError_Sens1_ScaleEnd_TermoNorm = x.GetTermoError (SensorIndex.Sens1, ScalePt.ScaleEnd, TermoPt.TermoNorm) 

    member x.TermoError_Sens1_ScaleEnd_TermoLow = x.GetTermoError (SensorIndex.Sens1, ScalePt.ScaleEnd, TermoPt.TermoLow) 

    member x.TermoError_Sens1_ScaleEnd_TermoHigh = x.GetTermoError (SensorIndex.Sens1, ScalePt.ScaleEnd, TermoPt.TermoHigh) 

    member x.TermoError_Sens2_ScaleBeg_TermoNorm = x.GetTermoError (SensorIndex.Sens2, ScalePt.ScaleBeg, TermoPt.TermoNorm) 

    member x.TermoError_Sens2_ScaleBeg_TermoLow = x.GetTermoError (SensorIndex.Sens2, ScalePt.ScaleBeg, TermoPt.TermoLow) 

    member x.TermoError_Sens2_ScaleBeg_TermoHigh = x.GetTermoError (SensorIndex.Sens2, ScalePt.ScaleBeg, TermoPt.TermoHigh) 

    member x.TermoError_Sens2_ScaleMid1_TermoNorm = x.GetTermoError (SensorIndex.Sens2, ScalePt.ScaleMid1, TermoPt.TermoNorm) 

    member x.TermoError_Sens2_ScaleMid1_TermoLow = x.GetTermoError (SensorIndex.Sens2, ScalePt.ScaleMid1, TermoPt.TermoLow) 

    member x.TermoError_Sens2_ScaleMid1_TermoHigh = x.GetTermoError (SensorIndex.Sens2, ScalePt.ScaleMid1, TermoPt.TermoHigh) 

    member x.TermoError_Sens2_ScaleEnd_TermoNorm = x.GetTermoError (SensorIndex.Sens2, ScalePt.ScaleEnd, TermoPt.TermoNorm) 

    member x.TermoError_Sens2_ScaleEnd_TermoLow = x.GetTermoError (SensorIndex.Sens2, ScalePt.ScaleEnd, TermoPt.TermoLow) 

    member x.TermoError_Sens2_ScaleEnd_TermoHigh = x.GetTermoError (SensorIndex.Sens2, ScalePt.ScaleEnd, TermoPt.TermoHigh) 

    member x.CCh0 = x.getPhysVarValueUi PhysVar.CCh0 

    member x.CCh1 = x.getPhysVarValueUi PhysVar.CCh1 

    member x.CCh2 = x.getPhysVarValueUi PhysVar.CCh2 

    member x.PkPa = x.getPhysVarValueUi PhysVar.PkPa 

    member x.Pmm = x.getPhysVarValueUi PhysVar.Pmm 

    member x.Tmcu = x.getPhysVarValueUi PhysVar.Tmcu 

    member x.Vbat = x.getPhysVarValueUi PhysVar.Vbat 

    member x.Vref = x.getPhysVarValueUi PhysVar.Vref 

    member x.Vmcu = x.getPhysVarValueUi PhysVar.Vmcu 

    member x.VdatP = x.getPhysVarValueUi PhysVar.VdatP 

    member x.CoutCh0 = x.getPhysVarValueUi PhysVar.CoutCh0 

    member x.TppCh0 = x.getPhysVarValueUi PhysVar.TppCh0 

    member x.ILOn0 = x.getPhysVarValueUi PhysVar.ILOn0 

    member x.ILOff0 = x.getPhysVarValueUi PhysVar.ILOff0 

    member x.Uw_Ch0 = x.getPhysVarValueUi PhysVar.Uw_Ch0 

    member x.Ur_Ch0 = x.getPhysVarValueUi PhysVar.Ur_Ch0 

    member x.WORK0 = x.getPhysVarValueUi PhysVar.WORK0 

    member x.REF0 = x.getPhysVarValueUi PhysVar.REF0 

    member x.Var1Ch0 = x.getPhysVarValueUi PhysVar.Var1Ch0 

    member x.Var2Ch0 = x.getPhysVarValueUi PhysVar.Var2Ch0 

    member x.Var3Ch0 = x.getPhysVarValueUi PhysVar.Var3Ch0 

    member x.FppCh0 = x.getPhysVarValueUi PhysVar.FppCh0 

    member x.CoutCh1 = x.getPhysVarValueUi PhysVar.CoutCh1 

    member x.TppCh1 = x.getPhysVarValueUi PhysVar.TppCh1 

    member x.ILOn1 = x.getPhysVarValueUi PhysVar.ILOn1 

    member x.ILOff1 = x.getPhysVarValueUi PhysVar.ILOff1 

    member x.Uw_Ch1 = x.getPhysVarValueUi PhysVar.Uw_Ch1 

    member x.Ur_Ch1 = x.getPhysVarValueUi PhysVar.Ur_Ch1 

    member x.WORK1 = x.getPhysVarValueUi PhysVar.WORK1 

    member x.REF1 = x.getPhysVarValueUi PhysVar.REF1 

    member x.Var1Ch1 = x.getPhysVarValueUi PhysVar.Var1Ch1 

    member x.Var2Ch1 = x.getPhysVarValueUi PhysVar.Var2Ch1 

    member x.Var3Ch1 = x.getPhysVarValueUi PhysVar.Var3Ch1 

    member x.FppCh1 = x.getPhysVarValueUi PhysVar.FppCh1 
