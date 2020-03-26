using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPayManager.Data
{
    public class GlobalVariable
    {
        public event Func<Task> OnChange;

        public bool IsCompanySelected { get; private set; }
        public int SelectedCompanyId { get; private set; }
        public string SelectedCompanyName { get; private set; }

        public GlobalVariable()
        {
            IsCompanySelected = false;
        }

        public void SetCompany(int id, string name)
        {
            if (id == 0) IsCompanySelected = false;
            else IsCompanySelected = true;

            SelectedCompanyId = id;
            SelectedCompanyName = name;
        }


        public async Task Update()
        {
            if (OnChange != null)
            {
                await OnChange.Invoke();
            }
        }
    }
}
