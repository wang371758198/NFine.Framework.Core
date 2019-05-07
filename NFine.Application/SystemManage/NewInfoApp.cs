using System;
using System.Collections.Generic;
using System.Text;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System.Linq;
using NFine.Code;


namespace NFine.Application.SystemManage
{
   public class NewInfoApp
    {
        private INewsInfoRepository service;
        public NewInfoApp(INewsInfoRepository newsInfoRepository)
        {
            this.service = newsInfoRepository;
        }
        public List<NewsInfoEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<NewsInfoEntity>();
            expression = expression.And(_ => _.F_DeleteMark != true);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                expression = expression.And(_ => _.F_Title.Contains(keyword));
            }
            return service.FindList(expression, pagination); ;
        }

        public void SubmitForm(NewsInfoEntity newsInfoEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                newsInfoEntity.Modify(keyValue);
                service.Update(newsInfoEntity);
            }
            else
            {
                newsInfoEntity.Create();
                service.Insert(newsInfoEntity);
            }
        }


        public NewsInfoEntity GetForm(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
                return null;
            var entity = service.FindEntity(keyValue);
            return entity;
        }

        public void DeleteForm(string keyValue)
        {
            NewsInfoEntity newsInfoEntity = new NewsInfoEntity();
            newsInfoEntity.Modify(keyValue);
            newsInfoEntity.Remove();
            service.Update(newsInfoEntity);
        }

        /// <summary>
        /// 根据文章类型取出前多少条信息
        /// </summary>
        /// <param name="type">1:</param>
        /// <param name="topNumber"></param>
        /// <returns></returns>
        public List<NewsInfoEntity> GetList(int type, int topNumber)
        {
            var expression = ExtLinq.True<NewsInfoEntity>();
            expression = expression.And(_ => _.F_Type == type && _.F_DeleteMark != true && _.F_Status == true);
            return service.IQueryable(expression)?.ToList();
        }

    }
}
