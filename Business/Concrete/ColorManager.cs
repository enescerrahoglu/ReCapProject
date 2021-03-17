﻿using Business.Abstract;
using Business.Constant;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }


        public IResult Add(Color color)
        {
            var result = BusinessRules.Run(CheckIfNameExists(color.ColorName));
            if (result != null)
            {
                return result;
            }
            _colorDal.Add(color);
            return new SuccessResult();
        }

        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.DeleteMessage);
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        public IResult Update(Color color)
        {
            var result = BusinessRules.Run(CheckIfNameExists(color.ColorName));
            if (result != null)
            {
                return result;
            }
            _colorDal.Update(color);
            return new SuccessResult(Messages.UpdateMessage);
        }

        private IResult CheckIfNameExists(string colorName)
        {
            var result = _colorDal.GetAll(c => c.ColorName.ToUpper() == colorName.ToUpper());
            if (result != null)
            {
                return new ErrorResult(Messages.ColorNameExists);
            }

            return new SuccessResult();
        }
    }
}