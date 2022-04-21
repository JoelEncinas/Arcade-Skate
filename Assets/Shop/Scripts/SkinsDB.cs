using System.Collections.Generic;
using UnityEngine;

public class SkinsDB 
{
    private Color32 playerColor;
    private Color32 topColor;
    private Color32 botColor;
    private Color32 skateColor;
    private Color32 leftTireColor;
    private Color32 rightTireColor;
    private Color32 hat1;
    private Color32 hat2;

    private List<Color32> skin0;
    private List<Color32> skin1;
    private List<Color32> skin2;
    private List<Color32> skin3;
    private List<Color32> skin4;
    private List<Color32> skin5;
    private List<Color32> skin6;
    private List<Color32> skin7;

    public List<Color32> GetSkin(int index)
    {
        // Classic
        playerColor = new Color32(192, 168, 144, 255);
        topColor = new Color32(72, 72, 48, 255);
        botColor = new Color32(37, 41, 44, 255);
        skateColor = new Color32(96, 72, 72, 255);
        leftTireColor = new Color32(28, 29, 30, 255);
        rightTireColor = new Color32(28, 29, 30, 255);
        hat1 = new Color32(0, 0, 0, 255);
        hat2 = new Color32(0, 0, 0, 255);

        skin0 = new List<Color32>()
        {
            playerColor,
            topColor,
            botColor,
            skateColor,
            leftTireColor,
            rightTireColor,
            hat1,
            hat2
        };

        // Zombie
        playerColor = new Color32(113, 134, 91, 255);
        topColor = new Color32(232, 218, 191, 255);
        botColor = new Color32(113, 134, 91, 255);
        skateColor = new Color32(120, 104, 89, 255);
        leftTireColor = new Color32(43, 43, 43, 255);
        rightTireColor = new Color32(43, 43, 43, 255);
        hat1 = new Color32(107, 37, 38, 255);
        hat2 = new Color32(107, 37, 38, 255);

        skin1 = new List<Color32>()
        {
            playerColor,
            topColor,
            botColor,
            skateColor,
            leftTireColor,
            rightTireColor,
            hat1,
            hat2
        };

        // Tony Hawk
        playerColor = new Color32(227, 166, 146, 255);
        topColor = new Color32(68, 87, 135, 255);
        botColor = new Color32(84, 75, 65, 255);
        skateColor = new Color32(14, 14, 18, 255);
        leftTireColor = new Color32(200, 200, 200, 255);
        rightTireColor = new Color32(200, 200, 200, 255);
        hat1 = new Color32(0, 0, 0, 0);
        hat2 = new Color32(65, 47, 34, 255);

        skin2 = new List<Color32>()
        {
            playerColor,
            topColor,
            botColor,
            skateColor,
            leftTireColor,
            rightTireColor,
            hat1,
            hat2
        };

        // Bart
        playerColor = new Color32(214, 169, 4, 255);
        topColor = new Color32(208, 71, 43, 255);
        botColor = new Color32(9, 111, 176, 255);
        skateColor = new Color32(61, 140, 37, 255);
        leftTireColor = new Color32(101, 39, 121, 255);
        rightTireColor = new Color32(101, 39, 121, 255);
        hat1 = new Color32(133, 33, 0, 255);
        hat2 = new Color32(133, 33, 0, 255);

        skin3 = new List<Color32>()
        {
            playerColor,
            topColor,
            botColor,
            skateColor,
            leftTireColor,
            rightTireColor,
            hat1,
            hat2
        };

        // Gangster
        playerColor = new Color32(154, 107, 56, 255);
        topColor = new Color32(27, 25, 26, 255);
        botColor = new Color32(27, 25, 26, 255);
        skateColor = new Color32(87, 53, 0, 255);
        leftTireColor = new Color32(219, 167, 61, 255);
        rightTireColor = new Color32(219, 167, 61, 255);
        hat1 = new Color32(0, 0, 0, 255);
        hat2 = new Color32(0, 0, 0, 255);

        skin4 = new List<Color32>()
        {
            playerColor,
            topColor,
            botColor,
            skateColor,
            leftTireColor,
            rightTireColor,
            hat1,
            hat2
        };

        // Ghost
        playerColor = new Color32(148, 162, 177, 200);
        topColor = new Color32(148, 162, 177, 200);
        botColor = new Color32(148, 162, 177, 200);
        skateColor = new Color32(148, 162, 177, 200);
        leftTireColor = new Color32(148, 162, 177, 200);
        rightTireColor = new Color32(148, 162, 177, 200);
        hat1 = new Color32(148, 162, 177, 200);
        hat2 = new Color32(148, 162, 177, 200);

        skin5 = new List<Color32>()
        {
            playerColor,
            topColor,
            botColor,
            skateColor,
            leftTireColor,
            rightTireColor,
            hat1,
            hat2
        };

        // Negative
        playerColor = new Color32(63, 87, 111, 255);
        topColor = new Color32(183, 183, 207, 255);
        botColor = new Color32(218, 214, 211, 255);
        skateColor = new Color32(159, 183, 183, 255);
        leftTireColor = new Color32(227, 226, 225, 255);
        rightTireColor = new Color32(227, 226, 225, 255);
        hat1 = new Color32(255, 255, 255, 255);
        hat2 = new Color32(255, 255, 255, 255);

        skin6 = new List<Color32>()
        {
            playerColor,
            topColor,
            botColor,
            skateColor,
            leftTireColor,
            rightTireColor,
            hat1,
            hat2
        };

        // Golden
        playerColor = new Color32(253, 205, 115, 255);
        topColor = new Color32(224, 172, 80, 255);
        botColor = new Color32(224, 172, 80, 255);
        skateColor = new Color32(176, 126, 48, 255);
        leftTireColor = new Color32(127, 90, 26, 255);
        rightTireColor = new Color32(127, 90, 26, 255);
        hat1 = new Color32(176, 126, 48, 255);
        hat2 = new Color32(176, 126, 48, 255);

        skin7 = new List<Color32>()
        {
            playerColor,
            topColor,
            botColor,
            skateColor,
            leftTireColor,
            rightTireColor,
            hat1,
            hat2
        };

        return index switch
        {
            0 => skin0,
            1 => skin1,
            2 => skin2,
            3 => skin3,
            4 => skin4,
            5 => skin5,
            6 => skin6,
            7 => skin7,
            _ => null,
        };
    }
}
