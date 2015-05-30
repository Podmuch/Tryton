//Abstract Model
//  base model for all models
//  contains the parameters for drawing (controller send them to view)
namespace Asteroids
{
    public abstract class AbstractModel
    {
        virtual public System.Object DrawParams
        {
            get;
            set;
        }
    }
}
