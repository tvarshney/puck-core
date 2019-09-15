﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using puck.core.Abstract;
using System.ComponentModel.DataAnnotations;
using puck.core.Attributes;
using Microsoft.AspNetCore.Mvc;
using puck.core.Abstract.EditorSettings;

namespace puck.core.Models.EditorSettings
{
    public enum PuckPickerSelectionType { node, variant, both };
    [Display(Name = "Puck Picker Editor Settings")]
    public class PuckPickerEditorSettings:I_Puck_Editor_Settings, I_Puck_Picker_Settings
    {
        public PuckPickerEditorSettings() {
            if (MaxPick == 0) MaxPick = 1;
        }
        public int MaxPick { get; set; }
        [UIHint("PuckPickerSelectionType")]
        public string SelectionType { get; set; }
        public bool AllowUnpublished { get; set; }
        public bool AllowDuplicates { get; set; }

        [UIHint("PuckPicker")]
        public List<PuckPicker> StartPath { get; set; }
        [HiddenInput(DisplayValue =false)]
        public string StartPathId { get; set; }
    }
}
