# ActionResponse

A wrapper object meant to unify all return types inside your application. 
Has built in markers and checkers for the result type making it easy to handle success or error inside the application.  

A real life example:  

```cs
    public async Task<ActionResponse> Update(PartnerUpdateRequest request)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var entityToUpdate = serviceBase.UnitOfWork.GetGenericRepository<Partner, int>()
                        .GetActive(e => e.Id == request.Id)
                        .Include(e => e.Currencies.Where(e => e.IsDeleted == DatabaseEntityStatus.Active))
                        .Include(e => e.PartnerTypes.Where(e => e.IsDeleted == DatabaseEntityStatus.Active))
                        .Include(e => e.Sites.Where(e => e.IsDeleted == DatabaseEntityStatus.Active))
                        .Include(e => e.TaxTypes.Where(e => e.IsDeleted == DatabaseEntityStatus.Active))
                        .First();

                    serviceBase.Mapper.Map(request, entityToUpdate);
                    if (ModifyPartnerCurrencies(entityToUpdate.Currencies, request.Currencies).IsNotSuccess(out ActionResponse childResponse))
                    {
                        return childResponse;
                    }

                    if (ModifyPartnerTypes(entityToUpdate.PartnerTypes, request.PartnerTypes).IsNotSuccess(out childResponse))
                    {
                        return childResponse;
                    }

                    if (ModifyPartnerSites(entityToUpdate.Sites, request.Sites).IsNotSuccess(out childResponse))
                    {
                        return childResponse;
                    }

                    if (ModifyPartnerTaxTypes(entityToUpdate.TaxTypes, request.TaxTypes).IsNotSuccess(out childResponse))
                    {
                        return childResponse;
                    }

                    var entityChooser = new EntityChooser
                    {
                        EntityId = request.Id.Value,
                        EntityType = EntityType.Partner
                    };

                    if ((await entityAddressService.ModifyEntityAddresses(entityChooser, request.Addresses))
                        .IsNotSuccess(out ActionResponse modifyResponse))
                    {
                        return modifyResponse.Convert<int>();
                    }

                    if ((await entityContactService.ModifyEntityContacts(entityChooser, request.Contacts))
                        .IsNotSuccess(out modifyResponse))
                    {
                        return modifyResponse.Convert<int>();
                    }

                    // Notice that this enables Cascade Soft Delete as well :D
                    await serviceBase.UnitOfWork.SaveAsync(request.ApplicationActionId);

                    scope.Complete();

                    return ActionResponse.Success(Code: 204);
                }
            }
            catch (Exception ex)
            {
                serviceBase.Logger.LogError(serviceBase.StringLocalizer.GetString(Resources.UpdateError), ex, JsonConvert.SerializeObject(request));
                return ActionResponse.Error(Message: serviceBase.StringLocalizer.GetString(Resources.UpdateError));
            }
        }
```