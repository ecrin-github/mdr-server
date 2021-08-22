using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.Contracts.v1.Requests.Query;
using mdr_server.Contracts.v1.Responses.StudyListResponse;
using mdr_server.Entities.FetchedData;
using mdr_server.Entities.Object;
using mdr_server.Entities.Study;

namespace mdr_server.Interfaces
{
    public interface IDataMapper
    {
        Task<List<StudyListResponse>> MapStudy(List<Study> studies);
        
        Task<List<StudyListResponse>> MapRawStudies(List<Study> studies);
        
        Task<List<StudyListResponse>> MapRawObjects(List<Object> objects);

        Task<List<StudyListResponse>> MapViaPublishedPaper(List<Object> objects);

        Task<List<StudyListResponse>> MapStudies(FetchedStudies fetchedStudies, FiltersListRequest filtersListRequest);
        Task<List<StudyListResponse>> MapObjects(FetchedObjects fetchedObjects, FiltersListRequest filtersListRequest);
    }
}