using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WebfleetAddressService;

namespace Soap12Webfleet {
    public static class Helpers {
        private const int requestQuotaReachedStatusCode = 8011;

        public static IEnumerable<T> GetResults<T>(this GenericServiceQueryOpResult result) where T : TransferObjectBase {
            if (result.errors?.Any() ?? false) {
                throw new ServiceQueryException(errorString(result.errors), result.statusCode);
            }
            if (result.results == null) {
                if (result.statusCode == requestQuotaReachedStatusCode) {
                    throw new QuotaExceededException(result.statusMessage);
                }
                throw new ServiceQueryException(result.statusMessage, result.statusCode);
            }

            return result.results.Cast<T>();
        }

        public static void EnsureSuccess(this GenericServiceWriteOpResult result) {
            if (result.errors?.Any() ?? false) {
                throw new ServiceQueryException(errorString(result.errors), result.statusCode);
            }
            if (result.failed != null) {
                if (result.statusCode == requestQuotaReachedStatusCode) {
                    throw new QuotaExceededException(result.statusMessage);
                }
                throw new ServiceQueryException(result.statusMessage, result.statusCode);
            }
        }

        private static string errorString(IEnumerable<ErrorInfo> errors) =>
            string.Join("\r\n", errors.Select((x, i) => $"error {i + 1}: {x.msg} | fieldName: {x.fieldName} | objectName: {x.objectName}"));
    }
}
