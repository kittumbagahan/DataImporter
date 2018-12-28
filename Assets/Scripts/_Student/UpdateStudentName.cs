using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStudentName
{

    string[] activityList = {
        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "0",
        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "3",
        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "6",
        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "9",
        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "12",

        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act2_coloring" + Module.PUZZLE.ToString() + "0",
        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act4" + Module.PUZZLE.ToString() + "1",
        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act5" + Module.PUZZLE.ToString() + "2",
        StoryBook.FAVORITE_BOX.ToString() + "favbox6_NEW" + Module.PUZZLE.ToString() + "3",

        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "0",
        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "3",
        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "6",
        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "9",
        StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "12",

        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "0",
        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "3",
        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "6",
        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "9",
        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "12",

        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_3" + Module.PUZZLE.ToString() + "0",
        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_4" + Module.PUZZLE.ToString() + "1",
        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_5" + Module.PUZZLE.ToString() + "2",
        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_6" + Module.PUZZLE.ToString() + "3",

        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "0",
        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "1",
        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "2",
        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "3",
        StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "4",

        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "0",
        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "3",
        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "6",
        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "9",
        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "12",

        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act2" + Module.PUZZLE.ToString() + "0",
        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act3" + Module.PUZZLE.ToString() + "0",
        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act3" + Module.PUZZLE.ToString() + "4",
        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act4" + Module.PUZZLE.ToString() + "0",

        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "0",
        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "4",
        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "10",
        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "18",
        StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "28",

        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "0",
        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "3",
        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "6",
        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "9",
        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "12",

        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_2" + Module.PUZZLE.ToString() + "0",
        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_3" + Module.PUZZLE.ToString() + "1",
        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_4" + Module.PUZZLE.ToString() + "2",
        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_5" + Module.PUZZLE.ToString() + "3",
        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_6" + Module.PUZZLE.ToString() + "4",

        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "0",
        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "1",
        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "2",
        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "3",
        StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "4",

        StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "0",
        StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "3",
        StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "6",
        StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "9",
        StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "12",

        StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act1" + Module.PUZZLE.ToString() + "0",
        StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct4" + Module.PUZZLE.ToString() + "1",
        StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct6" + Module.PUZZLE.ToString() + "2",
        StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct3" + Module.PUZZLE.ToString() + "3",

        StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION.ToString() + "-1",
        StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION.ToString() + "2",
        StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION.ToString() + "5",
        StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct5" + Module.OBSERVATION.ToString() + "0",
        StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct8" + Module.OBSERVATION.ToString() + "0",

        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "0",
        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "3",
        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "6",
        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "9",
        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "12",

        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act4" + Module.PUZZLE.ToString() + "0",
        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act6" + Module.PUZZLE.ToString() + "0",
        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act5" + Module.PUZZLE.ToString() + "0",
        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act5" + Module.PUZZLE.ToString() + "3",

        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "0",
        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "3",
        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "6",
        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "9",
        StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "12",

        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "0",
        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "3",
        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "6",
        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "9",
        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "12",

        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act2" + Module.PUZZLE.ToString() + "0",
        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act4" + Module.PUZZLE.ToString() + "0",
        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act5" + Module.PUZZLE.ToString() + "0",
        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act6" + Module.PUZZLE.ToString() + "0",

        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "0",
        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "4",
        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "10",
        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "18",
        StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "28",

        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "0",
        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "3",
        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "6",
        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "9",
        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "12",

        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act1" + Module.PUZZLE.ToString() + "0",
        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act2" + Module.PUZZLE.ToString() + "0",
        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act5" + Module.PUZZLE.ToString() + "0",
        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act5" + Module.PUZZLE.ToString() + "4",

        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act3" + Module.OBSERVATION.ToString() + "0",
        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act6" + Module.OBSERVATION.ToString() + "9",
        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act7" + Module.OBSERVATION.ToString() + "0",
        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act8" + Module.OBSERVATION.ToString() + "-1",
        StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act8" + Module.OBSERVATION.ToString() + "2",

        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act1" + Module.WORD.ToString() + "0",
        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD.ToString() + "0",
        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD.ToString() + "3",
        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD.ToString() + "6",
        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD.ToString() + "9",

        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act4" + Module.PUZZLE.ToString() + "0",
        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act5" + Module.PUZZLE.ToString() + "0",
        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act6" + Module.PUZZLE.ToString() + "0",
        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act7" + Module.PUZZLE.ToString() + "0",

        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "-1",
        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "3",
        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "7",
        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "11",
        StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "15",

        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "0",
        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "3",
        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "6",
        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "9",
        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "12",

        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_4" + Module.PUZZLE.ToString() + "0",
        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_5" + Module.PUZZLE.ToString() + "0",
        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_6" + Module.PUZZLE.ToString() + "0",
        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_7" + Module.PUZZLE.ToString() + "0",

        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_2" + Module.OBSERVATION.ToString() + "0",
        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_2" + Module.OBSERVATION.ToString() + "3",
        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION.ToString() + "0",
        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION.ToString() + "4",
        StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION.ToString() + "8",


    };

   

}
