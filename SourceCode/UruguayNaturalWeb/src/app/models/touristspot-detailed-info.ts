import {CategoryBasicInfo} from './category-basic-info';
import {RegionBasicInfo} from './region-basic-info';
import {CategoryDetailedInfo} from './category-detailed-info';

export interface TouristspotDetailedInfo {
    id: number;
    name: string;
    description: string;
    regionId: number;
    categories: CategoryDetailedInfo[];
    imageData: Uint8Array;

}

