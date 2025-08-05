
namespace QRTracker.Shared.Models;
public class QRCodeItem : BaseModel, ICloneable
{
    private int _Id = 0;
    public int Id
    {
        get
        {
            return _Id;
        }
        set
        {
            if (value != _Id)
            {
                _Id = value;
                OnPropertyChanged();
            }
        }
    }

    private string _URL = string.Empty;
    public string URL { get
        {
            return _URL;
        }
        set
        {
            if (value != _URL)
            {
                _URL = value;
                OnPropertyChanged();
            }
        }
    }

    public string Description { get; set; } = string.Empty;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
