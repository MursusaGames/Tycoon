using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatMenu : BaseMenu
{
    [SerializeField] private TextMeshProUGUI totalIncome;
    public double _totalIncome;

    [Header("PROCESSING LINE")]
    [SerializeField] private TextMeshProUGUI headCutMashine1Speed;
    [SerializeField] private TextMeshProUGUI headCutMashine1Income;
    [SerializeField] private TextMeshProUGUI headCutMashine2Speed;
    [SerializeField] private TextMeshProUGUI headCutMashine2Income;
    [SerializeField] private TextMeshProUGUI headCutMashineAllSpeed;
    [SerializeField] private TextMeshProUGUI headCutMashineAllIncome;
    [SerializeField] private Image headCut2BgImg;
    [SerializeField] private Image headCut2Img;
    [SerializeField] private Image headCut2BtnImg;

    [Header("LINE_1")]
    [SerializeField] private TextMeshProUGUI fishClean1Speed;
    [SerializeField] private TextMeshProUGUI fishClean1Income;
    [SerializeField] private TextMeshProUGUI steakMashineSpeed;
    [SerializeField] private TextMeshProUGUI steakMashineIncome;
    [SerializeField] private TextMeshProUGUI steakPackingMashineSpeed;
    [SerializeField] private TextMeshProUGUI steakPackingMashineIncome;
    [SerializeField] private TextMeshProUGUI line_1_AllSpeed;
    [SerializeField] private TextMeshProUGUI line_1_AllIncome;
    [SerializeField] private Image fishCleanBgImg;
    [SerializeField] private Image fishCleanImg;
    [SerializeField] private Image fishCleanBtnImg;
    [SerializeField] private Image steakBgImg;
    [SerializeField] private Image steakImg;
    [SerializeField] private Image steakBtnImg;
    [SerializeField] private Image packBgImg;
    [SerializeField] private Image packImg;
    [SerializeField] private Image packBtnImg;

    [Header("LINE_2")]
    [SerializeField] private TextMeshProUGUI fishClean2Speed;
    [SerializeField] private TextMeshProUGUI fishClean2Income;
    [SerializeField] private TextMeshProUGUI farshMashineSpeed;
    [SerializeField] private TextMeshProUGUI farshMashineIncome;
    [SerializeField] private TextMeshProUGUI farshPackingMashineSpeed;
    [SerializeField] private TextMeshProUGUI farshPackingMashineIncome;
    [SerializeField] private TextMeshProUGUI line_2_AllSpeed;
    [SerializeField] private TextMeshProUGUI line_2_AllIncome;
    [SerializeField] private Image fishClean2BgImg;
    [SerializeField] private Image fishClean2Img;
    [SerializeField] private Image fishClean2BtnImg;
    [SerializeField] private Image farshBgImg;
    [SerializeField] private Image farshImg;
    [SerializeField] private Image farshBtnImg;
    [SerializeField] private Image farshPackBgImg;
    [SerializeField] private Image farshPackImg;
    [SerializeField] private Image farshPackBtnImg;

    [Header("LINE_3")]
    [SerializeField] private TextMeshProUGUI fishClean3Speed;
    [SerializeField] private TextMeshProUGUI fishClean3Income;
    [SerializeField] private TextMeshProUGUI fileMashineSpeed;
    [SerializeField] private TextMeshProUGUI fileMashineIncome;
    [SerializeField] private TextMeshProUGUI filePackingMashineSpeed;
    [SerializeField] private TextMeshProUGUI filePackingMashineIncome;
    [SerializeField] private TextMeshProUGUI line_3_AllSpeed;
    [SerializeField] private TextMeshProUGUI line_3_AllIncome;
    [SerializeField] private Image fishClean3BgImg;
    [SerializeField] private Image fishClean3Img;
    [SerializeField] private Image fishClean3BtnImg;
    [SerializeField] private Image fileBgImg;
    [SerializeField] private Image fileImg;
    [SerializeField] private Image fileBtnImg;
    [SerializeField] private Image filePackBgImg;
    [SerializeField] private Image filePackImg;
    [SerializeField] private Image filePackBtnImg;

    public void OnStatButton()
    {
        InterfaceManager.SetCurrentMenu(MenuName.Main);             
    }
    private void OnEnable()
    {
        InitProcessingLineInfo();
        InitLine_1_Info();
        InitLine_2_Info();
        InitLine_3_Info();
    }

    private void InitProcessingLineInfo()
    {
        headCutMashine1Speed.text = data.userData.headCutingMashineSpeed.ToString();
        headCutMashine1Income.text = data.userData._headCutingMashinePromice;
        headCutMashine2Speed.text = data.userData.headCutingMashine > 1 ? data.userData.headCutingMashine2Speed.ToString() 
            :"-";
        headCutMashine2Income.text = data.userData.headCutingMashine > 1 ? data.userData._headCutingMashine2Promice:"-";
        double processingLine = data.userData.headCutingMashine > 1 ? 
            (data.userData.headCutingMashinePromice+data.userData.headCutingMashine2Promice): 
            data.userData.headCutingMashinePromice;
        _totalIncome = processingLine;
        headCutMashineAllIncome.text = Converter.instance.ConvertMoneyView(processingLine);
        headCutMashineAllSpeed.text = data.userData.headCutingMashine > 1 ? 
            (data.userData.headCutingMashineSpeed + data.userData.headCutingMashine2Speed).ToString() :
            data.userData.headCutingMashineSpeed.ToString();
        if(data.userData.headCutingMashine > 1)
        {
            ChangeColorToBlue(headCut2BgImg);
            ChangeColorToEazyBlue(headCut2BtnImg);
            Color c = headCut2Img.color;
            c.a = 255;
            headCut2Img.color = c;
        }

    }
    private void InitLine_1_Info()
    {
        if(data.userData.fishCleaningMashine > 0)
        {
            fishClean1Speed.text = data.userData.fishCleaningMashineSpeed.ToString();
            fishClean1Income.text = data.userData._fishCleaningMashinePromice;
            line_1_AllIncome.text = data.userData._fishCleaningMashinePromice;
            line_1_AllSpeed.text = data.userData.fishCleaningMashineSpeed.ToString();
            ChangeColorToBlue(fishCleanBgImg);
            ChangeColorToEazyBlue(fishCleanBtnImg);
            Color c = fishCleanImg.color;
            c.a = 255;
            fishCleanImg.color = c;
            _totalIncome += data.userData.fishCleaningMashinePromice;
        }
        else
        {
            fishClean1Speed.text = "-";
            fishClean1Income.text = "-";
        }

        if (data.userData.steakMashine > 0)
        {
            steakMashineSpeed.text = data.userData.steakMashineSpeed.ToString();
            steakMashineIncome.text = data.userData._steakMashinePromice;
            line_1_AllIncome.text = Converter.instance.ConvertMoneyView(data.userData.steakMashinePromice + data.userData.fishCleaningMashinePromice);
            line_1_AllSpeed.text = (data.userData.fishCleaningMashineSpeed+ data.userData.steakMashineSpeed).ToString();
            ChangeColorToBlue(steakBgImg);
            ChangeColorToEazyBlue(steakBtnImg);
            Color c = steakImg.color;
            c.a = 255;
            steakImg.color = c;
            _totalIncome += data.userData.steakMashinePromice;
        }
        else
        {
            steakMashineSpeed.text = "-";
            steakMashineIncome.text = "-";
        }
        if (data.userData.packingMashine > 0)
        {
            steakPackingMashineSpeed.text = data.userData.steakPackingMashineSpeed.ToString();
            steakPackingMashineIncome.text = data.userData._steakPackingMashinePromice;
            line_1_AllIncome.text = Converter.instance.ConvertMoneyView(data.userData.steakMashinePromice + data.userData.fishCleaningMashinePromice+
                data.userData.steakPackingMashinePromice);
            line_1_AllSpeed.text = (data.userData.fishCleaningMashineSpeed + data.userData.steakMashineSpeed+ data.userData.steakPackingMashineSpeed).ToString();
            ChangeColorToBlue(packBgImg);
            ChangeColorToEazyBlue(packBtnImg);
            Color c = packImg.color;
            c.a = 255;
            packImg.color = c;
            _totalIncome+= data.userData.steakPackingMashinePromice;
        }
        else
        {
            steakPackingMashineSpeed.text = "-";
            steakPackingMashineIncome.text = "-";
        }
    }
    private void InitLine_2_Info()
    {
        if (data.userData.fishCleaningMashine > 1)
        {
            fishClean2Speed.text = data.userData.fishCleaningMashine2Speed.ToString();
            fishClean2Income.text = data.userData._fishCleaningMashine2Promice;
            line_2_AllIncome.text = data.userData._fishCleaningMashine2Promice;
            line_2_AllSpeed.text = data.userData.fishCleaningMashine2Speed.ToString();
            ChangeColorToBlue(fishClean2BgImg);
            ChangeColorToEazyBlue(fishClean2BtnImg);
            Color c = fishClean2Img.color;
            c.a = 255;
            fishClean2Img.color = c;
            _totalIncome+= data.userData.fishCleaningMashine2Promice;
        }
        else
        {
            fishClean2Speed.text = "-";
            fishClean2Income.text = "-";
        }
        if (data.userData.farshMashine > 0)
        {
            farshMashineSpeed.text = data.userData.farshMashineSpeed.ToString();
            farshMashineIncome.text = data.userData._farshMashinePromice;
            line_2_AllIncome.text = Converter.instance.ConvertMoneyView(data.userData.farshMashinePromice + data.userData.fishCleaningMashine2Promice);
            line_2_AllSpeed.text = (data.userData.fishCleaningMashine2Speed + data.userData.farshMashineSpeed).ToString();
            ChangeColorToBlue(farshBgImg);
            ChangeColorToEazyBlue(farshBtnImg);
            Color c = farshImg.color;
            c.a = 255;
            farshImg.color = c;
            _totalIncome += data.userData.farshMashinePromice;
        }
        else
        {
            farshMashineSpeed.text = "-";
            farshMashineIncome.text = "-";
        }
        if (data.userData.packingMashine > 1)
        {
            farshPackingMashineSpeed.text = data.userData.farshPackingMashineSpeed.ToString();
            farshPackingMashineIncome.text = data.userData._farshPackingMashinePromice;
            line_2_AllIncome.text = Converter.instance.ConvertMoneyView(data.userData.farshMashinePromice + data.userData.fishCleaningMashine2Promice +
                data.userData.farshPackingMashinePromice);
            line_2_AllSpeed.text = (data.userData.fishCleaningMashine2Speed + data.userData.farshMashineSpeed + data.userData.farshPackingMashineSpeed).ToString();
            ChangeColorToBlue(farshPackBgImg);
            ChangeColorToEazyBlue(farshPackBtnImg);
            Color c = farshPackImg.color;
            c.a = 255;
            farshPackImg.color = c;
            _totalIncome += data.userData.steakPackingMashinePromice;
        }
        else
        {
            farshPackingMashineSpeed.text = "-";
            farshPackingMashineIncome.text = "-";
        }
    }
    private void InitLine_3_Info()
    {
        if (data.userData.fishCleaningMashine > 2)
        {
            fishClean3Speed.text = data.userData.fishCleaningMashine3Speed.ToString();
            fishClean3Income.text = data.userData._fishCleaningMashine3Promice;
            line_3_AllIncome.text = data.userData._fishCleaningMashine3Promice;
            line_3_AllSpeed.text = data.userData.fishCleaningMashine3Speed.ToString();
            ChangeColorToBlue(fishClean3BgImg);
            ChangeColorToEazyBlue(fishClean3BtnImg);
            Color c = fishClean3Img.color;
            c.a = 255;
            fishClean3Img.color = c;
            _totalIncome+= data.userData.fishCleaningMashine3Promice;
            
        }
        else
        {
            fishClean3Speed.text = "-";
            fishClean3Income.text = "-";
        }
        if (data.userData.fileMashine > 0)
        {
            fileMashineSpeed.text = data.userData.fileMashineSpeed.ToString();
            fileMashineIncome.text = data.userData._fileMashinePromice;
            line_3_AllIncome.text = Converter.instance.ConvertMoneyView(data.userData.fileMashinePromice + data.userData.fishCleaningMashine3Promice);
            line_3_AllSpeed.text = (data.userData.fishCleaningMashine3Speed + data.userData.fileMashineSpeed).ToString();
            ChangeColorToBlue(fileBgImg);
            ChangeColorToEazyBlue(fileBtnImg);
            Color c = fileImg.color;
            c.a = 255;
            fileImg.color = c;
            _totalIncome += data.userData.fileMashinePromice;
        }
        else
        {
            fileMashineSpeed.text = "-";
            fileMashineIncome.text = "-";
        }
        if (data.userData.packingMashine > 2)
        {
            filePackingMashineSpeed.text = data.userData.filePackingMashineSpeed.ToString();
            filePackingMashineIncome.text = data.userData._filePackingMashinePromice;
            line_3_AllIncome.text = Converter.instance.ConvertMoneyView(data.userData.fileMashinePromice + data.userData.fishCleaningMashine3Promice +
                data.userData.filePackingMashinePromice);
            line_3_AllSpeed.text = (data.userData.fishCleaningMashine3Speed + data.userData.fileMashineSpeed + data.userData.filePackingMashineSpeed).ToString();
            ChangeColorToBlue(filePackBgImg);
            ChangeColorToEazyBlue(filePackBtnImg);
            Color c = filePackImg.color;
            c.a = 255;
            filePackImg.color = c;
            _totalIncome += data.userData.filePackingMashinePromice;
        }
        else
        {
            filePackingMashineSpeed.text = "-";
            filePackingMashineIncome.text = "-";
        }
        totalIncome.text = Converter.instance.ConvertMoneyView(_totalIncome);
    }    

    private void ChangeColorToBlue(Image img)
    {
        string htmlValue = "#0D7EFF";        
        Color newCol;
        if (ColorUtility.TryParseHtmlString(htmlValue, out newCol))
        {
            img.color = newCol;
        }
    }    
    private void ChangeColorToEazyBlue(Image img)
    {
        string htmlValue = "#A3CDD9";
        Color newCol;
        if (ColorUtility.TryParseHtmlString(htmlValue, out newCol))
        {
            img.color = newCol;
        }
    }
}
