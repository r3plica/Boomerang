using Boomerang.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boomerang.Web
{
    public enum MoveDirection
    {
        Up,
        Down
    }

    public abstract class GenericType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public abstract void Save();
        public abstract int Move(int Order, MoveDirection Direction);

        public bool CreateBooleanFromMoveDirection(MoveDirection Direction)
        {
            bool bDirection = false;
            switch ((int)Direction)
            {
                case 1: bDirection = true; break;
                case 0: bDirection = false; break;
                default: bDirection = false; break;
            }
            return bDirection;
        }
    }

    public class Sector : GenericType
    {
        public int ClientId { get; set; }

        public override void Save()
        {
            if (Id == 0)
                this.Id = Sectors.Create(this);
            else
                Sectors.Edit(this);
        }

        public override int Move(int Order, MoveDirection Direction)
        {
            throw new NotImplementedException();
        }
    }

    public class SalaryRate : GenericType
    {

        #region Constructors

        public SalaryRate()
        {
        }

        public SalaryRate(int Id)
        {
            GenericType s = SalaryRates.Get(Id);
            this.Id = s.Id;
            this.Name = s.Name;
            this.Order = s.Order;
        }

        #endregion

        #region Methods

        public override void Save()
        {
            if (Id == 0)
                this.Id = SalaryRates.Create(this);
            else
                SalaryRates.Update(this);
        }

        public override int Move(int Order, MoveDirection Direction)
        {
            return SalaryRates.Move(Order, CreateBooleanFromMoveDirection(Direction));
        }

        #endregion

    }

    public class TransportType : GenericType
    {

        #region Constructors

        public TransportType()
        {
        }

        public TransportType(int Id)
        {
            GenericType t = TransportTypes.Get(Id);
            this.Id = t.Id;
            this.Name = t.Name;
            this.Order = t.Order;
        }

        #endregion

        #region Methods

        public override void Save()
        {
            if (Id == 0)
                this.Id = TransportTypes.Create(this);
            else
                TransportTypes.Update(this);
        }

        public override int Move(int Order, MoveDirection Direction)
        {
            return TransportTypes.Move(Order, CreateBooleanFromMoveDirection(Direction));
        }

        #endregion

    }

    public class SkillWorkType : GenericType
    {
        #region Methods

        public override void Save()
        {
            throw new NotImplementedException();
        }

        public override int Move(int Order, MoveDirection Direction)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
