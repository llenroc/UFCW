﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using UFCW.Helpers;
using UFCW.Services;
using UFCW.Services.Services.Claims;
using UFCW.Views.Pages.Claim;
using Xamarin.Forms;

namespace UFCW
{
    public class SearchClaimViewModel : INotifyPropertyChanged
    {
        private readonly static DateTime MinDate = DateTime.Parse("Jan 1 1970");
        private readonly static DateTime MaxDate = DateTime.Parse("Dec 31 2050");
        private static DateTime FromDefaultDate = DateTime.Parse("Jan 1 1980");
        public DateTime MinimumDate => MinDate;
        public DateTime MaximumDate => MaxDate;

		public DateTime toDate = DateTime.Now;
		public DateTime fromDate = FromDefaultDate;
      
        INavigation Navigation;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ClaimDetail> SearchedClaimsList;

        public ICommand DetailClaimCommand { get; set; }
        public ICommand SearchClaimCommand { get; set; }
        public ICommand ResetFiltersCommand { get; set; }

        //Pickers TODO temp code
              List<ClaimType> claimTypes = new List<ClaimType>
        {
        	 new ClaimType{
        		Text = ""
        	},
                  new ClaimType{
                      Text = "type1"
                  },
        	 new ClaimType{
        		Text = "type2"
        	},
        	 new ClaimType{
        		Text = "type3"
        	}

        };
              List<ClaimStatus> claimStatuses = new List<ClaimStatus>
        {
        	 new ClaimStatus{
        		Text = ""
        	},
                  new ClaimStatus{
                      Text = "ST1"
                  },
        	 new ClaimStatus{
        		Text = "ST2"
        	},
        	 new ClaimStatus{
        		Text = "ST3"
        	},
        };
              List<Patient> patientTypes = new List<Patient>
        {
        new Patient
        {
        	Text = ""
        },
                 new Patient
                 {
                     Text = "P1"
                 },
         new Patient
        {
        	Text = "P2"
        },
         new Patient
        {
        	Text = "P3"
        }
        };
        public List<ClaimType> ClaimTypes=>claimTypes;
        public List<ClaimStatus> ClaimStatuses=>claimStatuses;
        public List<Patient> PatientTypes=>patientTypes;

		//public ObservableCollection<ClaimType> ClaimTypes => claimTypes;
		//public ObservableCollection<ClaimStatus> ClaimStatuses;
		//public ObservableCollection<Patient> PatientTypes;
        public SearchClaimViewModel(INavigation mainPageNav)
        {
            SearchedClaimsList = new ObservableCollection<ClaimDetail>();
            Navigation = mainPageNav;
            SetupCommands();
   //         ClaimTypes = new ObservableCollection<ClaimType>();
			//ClaimStatuses = new ObservableCollection<ClaimStatus>();
		    //PatientTypes = new ObservableCollection<Patient>();
            LoadMore(); //TODO temp metod
		}
        /// <summary>
        /// Setups the commands for Resest,Search And Detail buttons.
        /// </summary>
        private void SetupCommands()
        {
			DetailClaimCommand = new Command(async (e) =>
		   {
			   ClaimDetail selectedItem = (e as ClaimDetail);
			   ClaimDetailPage detailPage = new ClaimDetailPage();
			   detailPage.BindingContext = selectedItem;
			   await Navigation.PushAsync(detailPage);
		   });
			ResetFiltersCommand = new Command(() =>
			{
				ClaimTypeSelectedIndex = 0;
				ClaimStatusSelectedIndex = 0;
				PatientSelectedIndex = 0;
                FirstDependentName = "";
                FromDate = FromDefaultDate;
                ToDate = DateTime.Now;
			});
			SearchClaimCommand = new Command(() =>
			{
                Patient selectedPatientTyoe = PatientTypes[patientSelectedIndex];
                ClaimType selectedCliamType = ClaimTypes[claimTypeSelectedIndex];
                ClaimStatus selectedClaimStatus = ClaimStatuses[claimStatusSelectedIndex];
                String dependentName = FirstDependentName;
                DateTime selectedFromDate = FromDate;
                DateTime selectedToDate = ToDate;

			});
        }


		#region Properties
		private bool isBusy = false;
		int claimTypeSelectedIndex;
		int claimStatusSelectedIndex;
		int patientSelectedIndex;
		String firstDepedentName;
        /// <summary>
        /// Gets or sets a value indicating for Activity Indicator.
        /// </summary>
        /// <value><c>true</c> if is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				if (isBusy != value)
				{
					isBusy = value;
					OnPropertyChanged("IsBusy");
				}
			}
		}
        public DateTime FromDate
		{
            get { return fromDate; }
			set
			{
				if (fromDate != value)
				{
                    fromDate = value;
					OnPropertyChanged("FromDate");
				}
			}
		}
		public DateTime ToDate
		{
			get { return toDate; }
			set
			{
				if (toDate != value)
				{
					toDate = value;
					OnPropertyChanged("ToDate");
				}
			}
		}
        /// <summary>
        /// Gets or sets the first name of the dependent.
        /// </summary>
        /// <value>The first name of the dependent.</value>
		public String FirstDependentName
		{
            get { return firstDepedentName; }
			set
			{
				if (firstDepedentName != value)
				{
					firstDepedentName = value;
					OnPropertyChanged("FirstDependentName");
				}
			}
		}
        /// <summary>
        /// Gets or sets the index of the claim type selected.
        /// </summary>
        /// <value>The index of the claim type selected.</value>
		public int ClaimTypeSelectedIndex
		{
            get { return claimTypeSelectedIndex; }
			set
			{
				if (claimTypeSelectedIndex != value)
				{
					claimTypeSelectedIndex = value;
                    OnPropertyChanged(nameof(ClaimTypeSelectedIndex));
				}
			}
		}
        /// <summary>
        /// Gets or sets the index of the claim status selected.
        /// </summary>
        /// <value>The index of the claim status selected.</value>
		public int ClaimStatusSelectedIndex
		{
			get { return claimStatusSelectedIndex; }
			set
			{
				if (claimStatusSelectedIndex != value)
				{
					claimStatusSelectedIndex = value;
					OnPropertyChanged("ClaimStatusSelectedIndex");
				}
			}
		}
        /// <summary>
        /// Gets or sets the index of the patient selected.
        /// </summary>
        /// <value>The index of the patient selected.</value>
		public int PatientSelectedIndex
		{
			get { return patientSelectedIndex; }
			set
			{
				if (patientSelectedIndex != value)
				{
					patientSelectedIndex = value;
					OnPropertyChanged("PatientSelectedIndex");
				}
			}
		}
        #endregion


        #region Server calls
        /// <summary>
        /// Gets the claim search filters option.
        /// </summary>
        /// <returns>The claim search filters.</returns>
        public async Task<ClaimFilters> GetClaimSearchFilters()
        {
			var claimService = new ClaimService();
            return await claimService.FetchSearchFilters(Settings.UserToken, Settings.UserSSN);
        }
		/// <summary>
		/// Applies the claim search.
		/// </summary>
		/// <returns>The claim search.</returns>
		/// <param name="claimType">Claim type.</param>
		/// <param name="claimStatus">Claim status.</param>
		/// <param name="fromDate">From date.</param>
		/// <param name="toDate">To date.</param>
		public async Task<ClaimDetail[]> ApplyClaimSearch(string claimType, string claimStatus, string fromDate, string toDate)
		{
			var claimService = new ClaimService();
			return await claimService.SearchClaim(Settings.UserToken, Settings.UserSSN, claimType,claimStatus,fromDate,toDate);
		}
        #endregion 

        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged(string propertyName)
		{
			var changed = PropertyChanged;
			if (changed != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
        public static int PageCount = 0;
        public static int TotalPages = 3;
        public bool isLoading;
        public void LoadMore()
		{
            if (PageCount <= TotalPages)
            {
                isLoading = true;
                PageCount++;
				ClaimDetail c1 = new ClaimDetail();
				c1.CLAIM_NUMBER = 1212;
				c1.INSURED_INITIALS = "abs";
				c1.PATIENT = "SELF";
				c1.DENTAL_SERVICE = "Dental";
				c1.CLAIM_STATUS = "Processed";
				c1.CLAIM_TYPE = "Dental";
				c1.DateCreated = "05/06/2017";//?????????

				//Claimant Information
				c1.INSURED_LAST_NAME = "RA BRANDEL";
				c1.ADDRESS1 = "800 QUACCO RD   247 ";
				c1.ADDRESS2 = "800 QUACCO RD   786";
				c1.CITY = "SAVANNAH";
				c1.STATE_CODE = 12345;
				c1.ZIP_CODE = 54000;
				c1.PATIENT = "SELF";
				c1.GENDER = "F";
				c1.AGE = 63;

				c1.EMPLOYER_NAME = "KROG.-SAVON-SV-PL D";
				c1.COV_FROM_DATE = "4/1/2002";
				c1.COV_TO_DATE = "10/1/2005";
				c1.FUND_ID = "AT MEAT RET-PL M-HI";
				c1.PLAN_ID = "D";
				c1.LOCAL_NUMBER = 1996;

				//Dental Summary
				c1.YTD_DENTAL = "162.00";
				c1.DENTAL_DEDUCTION = "25.00";
				c1.EXAM_CLEAN_DATE = "11/9/2005 12:00:00 AM";
				c1.LT_DENTAL = "1453.80";
				c1.LT_ORTHODONTIC = "0.00";
				c1.X_RAY_DATE = "11/9/2005";
				c1.PAY_REMARKS = "KROG.-SAVON-SV-PL D ";


				ClaimDetail c2 = new ClaimDetail();
				c2.CLAIM_NUMBER = 1213;
				c2.INSURED_INITIALS = "absdsd xfr";
				c2.PATIENT = "SELF";
				c2.DENTAL_SERVICE = "Dental";

				ClaimDetail c3 = new ClaimDetail();
				c3.CLAIM_NUMBER = 1214;
				c3.INSURED_INITIALS = "xyzw";
				c3.PATIENT = "SELF";
				c3.DENTAL_SERVICE = "Dental";

				ClaimDetail c4 = new ClaimDetail();
				c4.CLAIM_NUMBER = 545656;
				c4.INSURED_INITIALS = "stve";
				c4.PATIENT = "SELF";
				c4.DENTAL_SERVICE = "Dental";

				SearchedClaimsList.Add(c1);
				SearchedClaimsList.Add(c2);
				SearchedClaimsList.Add(c3);
				SearchedClaimsList.Add(c4);
                isLoading = false;
            }
			
			
		}
	}
}


