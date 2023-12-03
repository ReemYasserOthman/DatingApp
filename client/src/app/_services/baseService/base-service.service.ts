import { HttpService } from "../http/http.service";

export class BaseService<T> {
  private api: string;

  constructor(public httpService: HttpService, public path: string) {
    this.api = path;
  }

  GetAll() {
    return this.httpService.httpGet<T[]>(this.api);
  }

  GetById(id: number) {
    return this.httpService.httpGet<T>(this.api + id);
  }
  GetAllByName(name: string) {
    return this.httpService.httpGet<T[]>(this.api + name);
  }

  // GetByUid(uid: string) {
  //   return this.httpService.httpGet<T>(this.endPoint + "getByUid/" + uid);
  // }

  GetByApiName<T>(name: string) {
    return this.httpService.httpGet<T>(this.api + name);
  }

  Add(model: T) {
    return this.httpService.httpPost<T>(this.api + 'add', model);
  }

  Update(model: T) {
    return this.httpService.httpPut(this.api + 'update', model);
  }

  UpdateByName(model: T, username: string) {
    return this.httpService.httpPut(
      this.api + 'update/' + username,
      model
    );
  }

  Delete(id: number) {
    return this.httpService.httpDelete(this.api + 'delete/' + id);
  }
}
